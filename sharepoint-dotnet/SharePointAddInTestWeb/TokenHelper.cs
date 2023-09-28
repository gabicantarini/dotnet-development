using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;
using TokenValidationParameters = Microsoft.IdentityModel.Tokens.TokenValidationParameters;
using X509SigningCredentials = Microsoft.IdentityModel.Tokens.X509SigningCredentials;

namespace SharePointAddInTestWeb
{
    public static class TokenHelper
    {
        #region campos públicos

        /// <summary>
        /// Entidade do SharePoint.
        /// </summary>
        public const string SharePointPrincipal = "00000003-0000-0ff1-ce00-000000000000";

        /// <summary>
        /// Tempo de vida do token de acesso HighTrust, 12 horas.
        /// </summary>
        public static readonly TimeSpan HighTrustAccessTokenLifetime = TimeSpan.FromHours(12.0);

        #endregion public fields

        #region métodos públicos

        /// <summary>
        /// Recupera a cadeia de caracteres de token de contexto da solicitação especificada procurando nomes de parâmetros conhecidos no
        /// parâmetros de formulário via solicitação POST e na cadeia de caracteres de consulta. Retornará nulo se nenhum token de contexto for encontrado.
        /// </summary>
        /// <param name="request">HttpRequest em que procurar um token de contexto</param>
        /// <returns>A cadeia de caracteres do token de contexto</returns>
        public static string GetContextTokenFromRequest(HttpRequest request)
        {
            return GetContextTokenFromRequest(new HttpRequestWrapper(request));
        }

        /// <summary>
        /// Recupera a cadeia de caracteres de token de contexto da solicitação especificada procurando nomes de parâmetros conhecidos no
        /// parâmetros de formulário via solicitação POST e na cadeia de caracteres de consulta. Retornará nulo se nenhum token de contexto for encontrado.
        /// </summary>
        /// <param name="request">HttpRequest em que procurar um token de contexto</param>
        /// <returns>A cadeia de caracteres do token de contexto</returns>
        public static string GetContextTokenFromRequest(HttpRequestBase request)
        {
            string[] paramNames = { "AppContext", "AppContextToken", "AccessToken", "SPAppToken" };
            foreach (string paramName in paramNames)
            {
                if (!string.IsNullOrEmpty(request.Form[paramName]))
                {
                    return request.Form[paramName];
                }
                if (!string.IsNullOrEmpty(request.QueryString[paramName]))
                {
                    return request.QueryString[paramName];
                }
            }
            return null;
        }

        /// <summary>
        /// Validar se uma cadeia de caracteres de token de contexto especificada é destinada a este aplicativo com base nos parâmetros
        /// especificado em web.config. Os parâmetros de web.config usados para validação incluem ClientId,
        /// HostedAppHostNameOverride, HostedAppHostName, ClientSecret e Realm (se for especificado). Se HostedAppHostNameOverride estiver presente,
        /// será usado para validação. Caso contrário, se o <paramref name="appHostName"/> não for
        /// nulo, ele será usado para validação no lugar de HostedAppHostName de web.config. Se o token for inválido, um
        /// exceção será lançada. Se o identificador for válido, a URL de metadados STS estáticos do TokenHelper será atualizada com base no conteúdo do token
        /// e um JwtSecurityToken com base no token de contexto é retornado.
        /// </summary>
        /// <param name="contextTokenString">O token de contexto a validar</param>
        /// <param name="appHostName">A autoridade da URL, consistindo de endereço IP ou nome de host do DNS (Sistema de Nomes de Domínio) e o número da porta a usar para validação da audiência do token.
        /// Se for nulo, a configuração HostedAppHostName de web.config será usada em seu lugar. A configuração HostedAppHostNameOverride de web.config será usada se estiver presente
        /// para validação no lugar de <paramref name="appHostName"/> .</param>
        /// <returns>Um JwtSecurityToken com base no token de contexto.</returns>
        public static SharePointContextToken ReadAndValidateContextToken(string contextTokenString, string appHostName = null)
        {
            List<SymmetricSecurityKey> securityKeys = new List<SymmetricSecurityKey>
            {
                new SymmetricSecurityKey(Convert.FromBase64String(ClientSecret))
            };

            if (!string.IsNullOrEmpty(SecondaryClientSecret))
            {
                securityKeys.Add(new SymmetricSecurityKey(Convert.FromBase64String(SecondaryClientSecret)));
            }

            JwtSecurityTokenHandler tokenHandler = CreateJwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false, // validado abaixo
                IssuerSigningKeys = securityKeys // validar a assinatura
            };

            tokenHandler.ValidateToken(contextTokenString, parameters, out Microsoft.IdentityModel.Tokens.SecurityToken securityToken);
            SharePointContextToken token = SharePointContextToken.Create(securityToken as JwtSecurityToken);

            string stsAuthority = (new Uri(token.SecurityTokenServiceUri)).Authority;
            int firstDot = stsAuthority.IndexOf('.');

            GlobalEndPointPrefix = stsAuthority.Substring(0, firstDot);
            AcsHostUrl = stsAuthority.Substring(firstDot + 1);

            string[] acceptableAudiences;
            if (!String.IsNullOrEmpty(HostedAppHostNameOverride))
            {
                acceptableAudiences = HostedAppHostNameOverride.Split(';');
            }
            else if (appHostName == null)
            {
                acceptableAudiences = new[] { HostedAppHostName };
            }
            else
            {
                acceptableAudiences = new[] { appHostName };
            }

            bool validationSuccessful = false;
            string realm = Realm ?? token.Realm;
            foreach (var audience in acceptableAudiences)
            {
                string principal = GetFormattedPrincipal(ClientId, audience, realm);
                if (token.Audiences.First<string>(item => StringComparer.OrdinalIgnoreCase.Equals(item, principal)) != null)
                {
                    validationSuccessful = true;
                    break;
                }
            }

            if (!validationSuccessful)
            {
                throw new AudienceUriValidationFailedException(
                    String.Format(CultureInfo.CurrentCulture,
                    "\"{0}\" is not the intended audience \"{1}\"", String.Join(";", acceptableAudiences),
                    String.Join(";", token.Audiences.ToArray<string>())));
            }

            return token;
        }

        /// <summary>
        /// Recupera um token de acesso do ACS para chamar a origem do token de contexto especificado no
        /// targetHost especificado. O targetHost deve ser registrado pela entidade que enviou o token de contexto.
        /// </summary>
        /// <param name="contextToken">Token de contexto emitido pelo público-alvo do token de acesso</param>
        /// <param name="targetHost">Autoridade de URL da entidade de destino</param>
        /// <returns>Um token de acesso com uma audiência correspondente à origem do token de contexto</returns>
        public static OAuthTokenResponse GetAccessToken(SharePointContextToken contextToken, string targetHost)
        {
            string targetPrincipalName = contextToken.TargetPrincipalName;

            // Extrair o refreshToken o token de contexto
            string refreshToken = contextToken.RefreshToken;

            if (String.IsNullOrEmpty(refreshToken))
            {
                return null;
            }

            string targetRealm = Realm ?? contextToken.Realm;

            return GetAccessToken(refreshToken,
                                  targetPrincipalName,
                                  targetHost,
                                  targetRealm);
        }

        /// <summary>
        /// Usa o código de autorização especificado para recuperar um token de acesso do ACS para chamar a entidade de segurança especificada
        /// no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
        /// nulo, a configuração "Realm" em web.config será usada em seu lugar.
        /// </summary>
        /// <param name="authorizationCode">Código de autorização a trocar pelo token de acesso</param>
        /// <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
        /// <param name="targetHost">Autoridade de URL da entidade de destino</param>
        /// <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
        /// <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
        /// <returns>Um token de acesso com uma audiência da entidade de destino</returns>
        public static OAuthTokenResponse GetAccessToken(
            string authorizationCode,
            string targetPrincipalName,
            string targetHost,
            string targetRealm,
            Uri redirectUri)
        {
            if (targetRealm == null)
            {
                targetRealm = Realm;
            }

            string resource = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm);
            string clientId = GetFormattedPrincipal(ClientId, null, targetRealm);
            string acsUri = AcsMetadataParser.GetStsUrl(targetRealm);
            OAuthTokenResponse oauthResponse = null;

            try
            {
                oauthResponse = OAuthClient.GetAccessTokenWithAuthorizationCode(acsUri, clientId, ClientSecret,
                    authorizationCode, redirectUri.AbsoluteUri, resource);
            }
            catch (WebException wex)
            {
                if (!string.IsNullOrEmpty(SecondaryClientSecret))
                {
                    oauthResponse = OAuthClient.GetAccessTokenWithAuthorizationCode(acsUri, clientId, SecondaryClientSecret,
                        authorizationCode, redirectUri.AbsoluteUri, resource);
                }
                else
                {
                    using (StreamReader sr = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        string responseText = sr.ReadToEnd();
                        throw new WebException(wex.Message + " - " + responseText, wex);
                    }
                }
            }

            return oauthResponse;
        }

        /// <summary>
        /// Usa o token de atualização especificado para recuperar um token de acesso do ACS para chamar a entidade de segurança especificada
        /// no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
        /// nulo, a configuração "Realm" em web.config será usada em seu lugar.
        /// </summary>
        /// <param name="refreshToken">Atualizar token a trocar pelo token de acesso</param>
        /// <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
        /// <param name="targetHost">Autoridade de URL da entidade de destino</param>
        /// <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
        /// <returns>Um token de acesso com uma audiência da entidade de destino</returns>
        public static OAuthTokenResponse GetAccessToken(
            string refreshToken,
            string targetPrincipalName,
            string targetHost,
            string targetRealm)
        {
            if (targetRealm == null)
            {
                targetRealm = Realm;
            }

            string resource = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm);
            string clientId = GetFormattedPrincipal(ClientId, null, targetRealm);
            string acsUri = AcsMetadataParser.GetStsUrl(targetRealm);
            OAuthTokenResponse oauthResponse = null;

            try
            {
                oauthResponse = OAuthClient.GetAccessTokenWithRefreshToken(acsUri, clientId, ClientSecret, refreshToken, resource);
            }
            catch (WebException wex)
            {
                if (!string.IsNullOrEmpty(SecondaryClientSecret))
                {
                    oauthResponse = OAuthClient.GetAccessTokenWithRefreshToken(acsUri, clientId, SecondaryClientSecret, refreshToken, resource);
                }
                else
                {
                    using (StreamReader sr = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        string responseText = sr.ReadToEnd();
                        throw new WebException(wex.Message + " - " + responseText, wex);
                    }
                }
            }

            return oauthResponse;
        }

        /// <summary>
        /// Recupera um token de acesso somente de aplicativo do ACS para chamar a entidade especificada
        /// no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
        /// nulo, a configuração "Realm" em web.config será usada em seu lugar.
        /// </summary>
        /// <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
        /// <param name="targetHost">Autoridade de URL da entidade de destino</param>
        /// <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
        /// <returns>Um token de acesso com uma audiência da entidade de destino</returns>
        public static OAuthTokenResponse GetAppOnlyAccessToken(
            string targetPrincipalName,
            string targetHost,
            string targetRealm)
        {

            if (targetRealm == null)
            {
                targetRealm = Realm;
            }

            string resource = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm);
            string clientId = GetFormattedPrincipal(ClientId, HostedAppHostName, targetRealm);
            string acsUri = AcsMetadataParser.GetStsUrl(targetRealm);
            OAuthTokenResponse oauthResponse = null;

            try
            {
                oauthResponse = OAuthClient.GetAccessTokenWithClientCredentials(acsUri, clientId, ClientSecret, resource);
            }
            catch (WebException wex)
            {
                if (!string.IsNullOrEmpty(SecondaryClientSecret))
                {
                    oauthResponse = OAuthClient.GetAccessTokenWithClientCredentials(acsUri, clientId, SecondaryClientSecret, resource);
                }
                else
                {
                    using (StreamReader sr = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        string responseText = sr.ReadToEnd();
                        throw new WebException(wex.Message + " - " + responseText, wex);
                    }
                }
            }

            return oauthResponse;
        }

        /// <summary>
        /// Cria um contexto de cliente com base nas propriedades de um receptor de eventos remoto
        /// </summary>
        /// <param name="properties">Propriedades de um receptor de eventos remoto</param>
        /// <returns>Um ClientContext pronto para chamar a Web em que o evento foi originado</returns>
        public static ClientContext CreateRemoteEventReceiverClientContext(SPRemoteEventProperties properties)
        {
            Uri sharepointUrl;
            if (properties.ListEventProperties != null)
            {
                sharepointUrl = new Uri(properties.ListEventProperties.WebUrl);
            }
            else if (properties.ItemEventProperties != null)
            {
                sharepointUrl = new Uri(properties.ItemEventProperties.WebUrl);
            }
            else if (properties.WebEventProperties != null)
            {
                sharepointUrl = new Uri(properties.WebEventProperties.FullUrl);
            }
            else
            {
                return null;
            }

            if (IsHighTrustApp())
            {
                return GetS2SClientContextWithWindowsIdentity(sharepointUrl, null);
            }

            return CreateAcsClientContextForUrl(properties, sharepointUrl);
        }

        /// <summary>
        /// Cria um contexto de cliente com base nas propriedades de um evento de suplemento
        /// </summary>
        /// <param name="properties">Propriedades de um de evento e suplemento</param>
        /// <param name="useAppWeb">True para usar o aplicativo Web como destino, false para usar o host Web como destino</param>
        /// <returns>Um ClientContext pronto para chamar o aplicativo Web ou o site pai</returns>
        public static ClientContext CreateAppEventClientContext(SPRemoteEventProperties properties, bool useAppWeb)
        {
            if (properties.AppEventProperties == null)
            {
                return null;
            }

            Uri sharepointUrl = useAppWeb ? properties.AppEventProperties.AppWebFullUrl : properties.AppEventProperties.HostWebFullUrl;
            if (IsHighTrustApp())
            {
                return GetS2SClientContextWithWindowsIdentity(sharepointUrl, null);
            }

            return CreateAcsClientContextForUrl(properties, sharepointUrl);
        }

        /// <summary>
        /// Recupera um token de acesso do ACS usando o código de autorização especificado e usa esse token de acesso para
        /// criar um contexto de cliente
        /// </summary>
        /// <param name="targetUrl">URL do site do SharePoint de destino</param>
        /// <param name="authorizationCode">Código de autorização a usar ao recuperar o token de acesso do ACS</param>
        /// <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
        /// <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
        public static ClientContext GetClientContextWithAuthorizationCode(
            string targetUrl,
            string authorizationCode,
            Uri redirectUri)
        {
            return GetClientContextWithAuthorizationCode(targetUrl, SharePointPrincipal, authorizationCode, GetRealmFromTargetUrl(new Uri(targetUrl)), redirectUri);
        }

        /// <summary>
        /// Recupera um token de acesso do ACS usando o código de autorização especificado e usa esse token de acesso para
        /// criar um contexto de cliente
        /// </summary>
        /// <param name="targetUrl">URL do site do SharePoint de destino</param>
        /// <param name="targetPrincipalName">Nome da entidade do SharePoint de destino</param>
        /// <param name="authorizationCode">Código de autorização a usar ao recuperar o token de acesso do ACS</param>
        /// <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
        /// <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
        /// <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
        public static ClientContext GetClientContextWithAuthorizationCode(
            string targetUrl,
            string targetPrincipalName,
            string authorizationCode,
            string targetRealm,
            Uri redirectUri)
        {
            Uri targetUri = new Uri(targetUrl);

            string accessToken =
                GetAccessToken(authorizationCode, targetPrincipalName, targetUri.Authority, targetRealm, redirectUri).AccessToken;

            return GetClientContextWithAccessToken(targetUrl, accessToken);
        }

        /// <summary>
        /// Usa o token de acesso especificado criar um contexto de cliente
        /// </summary>
        /// <param name="targetUrl">URL do site do SharePoint de destino</param>
        /// <param name="accessToken">O token de acesso a ser usado ao chamar a targetUrl especificada</param>
        /// <returns>Um ClientContext pronto para chamar a targetUrl com o token de acesso especificado</returns>
        public static ClientContext GetClientContextWithAccessToken(string targetUrl, string accessToken)
        {
            ClientContext clientContext = new ClientContext(targetUrl);

            clientContext.AuthenticationMode = ClientAuthenticationMode.Anonymous;
            clientContext.FormDigestHandlingEnabled = false;
            clientContext.ExecutingWebRequest +=
                delegate (object oSender, WebRequestEventArgs webRequestEventArgs)
                {
                    webRequestEventArgs.WebRequestExecutor.RequestHeaders["Authorization"] =
                        "Bearer " + accessToken;
                };

            return clientContext;
        }

        /// <summary>
        /// Recupera um token de acesso do ACS usando o token de contexto especificado e usa esse token de acesso para criar
        /// um contexto de cliente
        /// </summary>
        /// <param name="targetUrl">URL do site do SharePoint de destino</param>
        /// <param name="contextTokenString">Token de contexto recebido do site do SharePoint de destino</param>
        /// <param name="appHostUrl">Autoridade da URL do suplemento hospedado. Se for nula, o valor em HostedAppHostName
        /// de web.config será usado em seu lugar</param>
        /// <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
        public static ClientContext GetClientContextWithContextToken(
            string targetUrl,
            string contextTokenString,
            string appHostUrl)
        {
            SharePointContextToken contextToken = ReadAndValidateContextToken(contextTokenString, appHostUrl);

            Uri targetUri = new Uri(targetUrl);

            string accessToken = GetAccessToken(contextToken, targetUri.Authority).AccessToken;

            return GetClientContextWithAccessToken(targetUrl, accessToken);
        }

        /// <summary>
        /// Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar consentimento e retornar
        /// um código de autorização.
        /// </summary>
        /// <param name="contextUrl">URL absoluta do site do SharePoint</param>
        /// <param name="scope">Permissões delimitadas por espaço a solicitar do site do SharePoint em formato "abreviado"
        /// (por exemplo, "Web.Read Site.Write")</param>
        /// <returns>URL da página de autorização OAuth do site do SharePoint</returns>
        public static string GetAuthorizationUrl(string contextUrl, string scope)
        {
            return string.Format(
                "{0}{1}?IsDlg=1&client_id={2}&scope={3}&response_type=code",
                EnsureTrailingSlash(contextUrl),
                AuthorizationPage,
                ClientId,
                scope);
        }

        /// <summary>
        /// Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar consentimento e retornar
        /// um código de autorização.
        /// </summary>
        /// <param name="contextUrl">URL absoluta do site do SharePoint</param>
        /// <param name="scope">Permissões delimitadas por espaço a solicitar do site do SharePoint em formato "abreviado"
        /// (por exemplo, "Web.Read Site.Write")</param>
        /// <param name="redirectUri">O URI para o qual o SharePoint deve redirecionar o navegador depois que o consentimento é
        /// concedido</param>
        /// <returns>URL da página de autorização OAuth do site do SharePoint</returns>
        public static string GetAuthorizationUrl(string contextUrl, string scope, string redirectUri)
        {
            return string.Format(
                "{0}{1}?IsDlg=1&client_id={2}&scope={3}&response_type=code&redirect_uri={4}",
                EnsureTrailingSlash(contextUrl),
                AuthorizationPage,
                ClientId,
                scope,
                redirectUri);
        }

        /// <summary>
        /// Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar um novo token de contexto.
        /// </summary>
        /// <param name="contextUrl">URL absoluta do site do SharePoint</param>
        /// <param name="redirectUri">URI para o qual o SharePoint deve redirecionar o navegador com um token de contexto</param>
        /// <returns>URL da página de redirecionamento do token de contexto do site do SharePoint</returns>
        public static string GetAppContextTokenRequestUrl(string contextUrl, string redirectUri)
        {
            return string.Format(
                "{0}{1}?client_id={2}&redirect_uri={3}",
                EnsureTrailingSlash(contextUrl),
                RedirectPage,
                ClientId,
                redirectUri);
        }

        /// <summary>
        /// Recupera um token de acesso S2S assinado pelo certificado privado do aplicativo em nome do especificado
        /// WindowsIdentity e destinado ao SharePoint no targetApplicationUri. Se nenhum Realm estiver especificado em
        /// web.config, um desafio de autenticação será emitido ao targetApplicationUri para que este o descubra.
        /// </summary>
        /// <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
        /// <param name="identity">Identidade do Windows do usuário em cujo nome criar o token de acesso</param>
        /// <returns>Um token de acesso com uma audiência da entidade de destino</returns>
        public static string GetS2SAccessTokenWithWindowsIdentity(
            Uri targetApplicationUri,
            WindowsIdentity identity)
        {
            string realm = string.IsNullOrEmpty(Realm) ? GetRealmFromTargetUrl(targetApplicationUri) : Realm;

            Claim[] claims = identity != null ? GetClaimsWithWindowsIdentity(identity) : null;

            return GetS2SAccessTokenWithClaims(targetApplicationUri.Authority, realm, claims);
        }

        /// <summary>
        /// Recupera um contexto do cliente S2S com um token de acesso assinado pelo certificado privado do aplicativo em
        /// em nome do WindowsIdentity especificado e pretendido para o aplicativo no targetApplicationUri usando o
        /// targetRealm. Se nenhum Realm for especificado em web.config, um desafio de autenticação será emitido ao
        /// targetApplicationUri para que este o descubra.
        /// </summary>
        /// <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
        /// <param name="identity">Identidade do Windows do usuário em cujo nome criar o token de acesso</param>
        /// <returns>Um ClientContext usando um token de acesso com uma audiência do aplicativo de destino</returns>
        public static ClientContext GetS2SClientContextWithWindowsIdentity(
            Uri targetApplicationUri,
            WindowsIdentity identity)
        {
            string realm = string.IsNullOrEmpty(Realm) ? GetRealmFromTargetUrl(targetApplicationUri) : Realm;

            Claim[] claims = identity != null ? GetClaimsWithWindowsIdentity(identity) : null;

            string accessToken = GetS2SAccessTokenWithClaims(targetApplicationUri.Authority, realm, claims);

            return GetClientContextWithAccessToken(targetApplicationUri.ToString(), accessToken);
        }

        /// <summary>
        /// Obter o realm de autenticação do SharePoint
        /// </summary>
        /// <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
        /// <returns>Representação do GUID do realm em cadeia de caracteres</returns>
        public static string GetRealmFromTargetUrl(Uri targetApplicationUri)
        {
            WebRequest request = WebRequest.Create(targetApplicationUri + "/_vti_bin/client.svc");
            request.Headers.Add("Authorization: Bearer ");

            try
            {
                using (request.GetResponse())
                {
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return null;
                }

                string bearerResponseHeader = e.Response.Headers["WWW-Authenticate"];
                if (string.IsNullOrEmpty(bearerResponseHeader))
                {
                    return null;
                }

                const string bearer = "Bearer realm=\"";
                int bearerIndex = bearerResponseHeader.IndexOf(bearer, StringComparison.Ordinal);
                if (bearerIndex < 0)
                {
                    return null;
                }

                int realmIndex = bearerIndex + bearer.Length;

                if (bearerResponseHeader.Length >= realmIndex + 36)
                {
                    string targetRealm = bearerResponseHeader.Substring(realmIndex, 36);

                    Guid realmGuid;

                    if (Guid.TryParse(targetRealm, out realmGuid))
                    {
                        return targetRealm;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Determina se este é um suplemento de alta confiança.
        /// </summary>
        /// <returns>True se este for um suplemento de alta confiança.</returns>
        public static bool IsHighTrustApp()
        {
            return SigningCredentials != null;
        }

        /// <summary>
        /// Garante que a URL especificada termina com '/' se ela não for nula ou vazia.
        /// </summary>
        /// <param name="url">A URL.</param>
        /// <returns>A URL terminando com '/', se ela não for nula ou vazia.</returns>
        public static string EnsureTrailingSlash(string url)
        {
            if (!string.IsNullOrEmpty(url) && url[url.Length - 1] != '/')
            {
                return url + "/";
            }

            return url;
        }

        /// <summary>
        /// Retorna o horário da Época atual em segundos
        /// </summary>
        /// <returns>Horário da Época em segundos</returns>
        public static long EpochTimeNow()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1).ToUniversalTime()).TotalSeconds;
        }

        #endregion

        #region campos particulares

        //
        // Constantes de Configuração
        //

        private const string AuthorizationPage = "_layouts/15/OAuthAuthorize.aspx";
        private const string RedirectPage = "_layouts/15/AppRedirect.aspx";
        private const string AcsPrincipalName = "00000001-0000-0000-c000-000000000000";
        private const string AcsMetadataEndPointRelativeUrl = "metadata/json/1";
        private const string S2SProtocol = "OAuth2";
        private const string DelegationIssuance = "DelegationIssuance1.0";
        private const string NameIdentifierClaimType = "nameid";
        private const string TrustedForImpersonationClaimType = "trustedfordelegation";
        private const string ActorTokenClaimType = "actortoken";

        //
        // Constantes de Ambiente
        //

        private static string GlobalEndPointPrefix = "accounts";
        private static string AcsHostUrl = "accesscontrol.windows.net";

        //
        // Configuração de suplemento hospedado
        //
        private static readonly string ClientId = string.IsNullOrEmpty(WebConfigurationManager.AppSettings.Get("ClientId")) ? WebConfigurationManager.AppSettings.Get("HostedAppName") : WebConfigurationManager.AppSettings.Get("ClientId");
        private static readonly string IssuerId = string.IsNullOrEmpty(WebConfigurationManager.AppSettings.Get("IssuerId")) ? ClientId : WebConfigurationManager.AppSettings.Get("IssuerId");
        private static readonly string HostedAppHostNameOverride = WebConfigurationManager.AppSettings.Get("HostedAppHostNameOverride");
        private static readonly string HostedAppHostName = WebConfigurationManager.AppSettings.Get("HostedAppHostName");
        private static readonly string ClientSecret = string.IsNullOrEmpty(WebConfigurationManager.AppSettings.Get("ClientSecret")) ? WebConfigurationManager.AppSettings.Get("HostedAppSigningKey") : WebConfigurationManager.AppSettings.Get("ClientSecret");
        private static readonly string SecondaryClientSecret = WebConfigurationManager.AppSettings.Get("SecondaryClientSecret");
        private static readonly string Realm = WebConfigurationManager.AppSettings.Get("Realm");
        private static readonly string ServiceNamespace = WebConfigurationManager.AppSettings.Get("Realm");

        private static readonly string ClientSigningCertificatePath = WebConfigurationManager.AppSettings.Get("ClientSigningCertificatePath");
        private static readonly string ClientSigningCertificatePassword = WebConfigurationManager.AppSettings.Get("ClientSigningCertificatePassword");
        private static readonly X509Certificate2 ClientCertificate = (string.IsNullOrEmpty(ClientSigningCertificatePath) || string.IsNullOrEmpty(ClientSigningCertificatePassword)) ? null : new X509Certificate2(ClientSigningCertificatePath, ClientSigningCertificatePassword);
        private static readonly X509SigningCredentials SigningCredentials = (ClientCertificate == null) ? null : new X509SigningCredentials(ClientCertificate, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256);

        #endregion

        #region métodos privados

        private static ClientContext CreateAcsClientContextForUrl(SPRemoteEventProperties properties, Uri sharepointUrl)
        {
            string contextTokenString = properties.ContextToken;

            if (String.IsNullOrEmpty(contextTokenString))
            {
                return null;
            }

            SharePointContextToken contextToken = ReadAndValidateContextToken(contextTokenString, OperationContext.Current.IncomingMessageHeaders.To.Host);
            string accessToken = GetAccessToken(contextToken, sharepointUrl.Authority).AccessToken;

            return GetClientContextWithAccessToken(sharepointUrl.ToString(), accessToken);
        }

        private static string GetAcsMetadataEndpointUrl()
        {
            return Path.Combine(GetAcsGlobalEndpointUrl(), AcsMetadataEndPointRelativeUrl);
        }

        private static string GetFormattedPrincipal(string principalName, string hostName, string realm)
        {
            if (!String.IsNullOrEmpty(hostName))
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}/{1}@{2}", principalName, hostName, realm);
            }

            return String.Format(CultureInfo.InvariantCulture, "{0}@{1}", principalName, realm);
        }

        private static string GetAcsPrincipalName(string realm)
        {
            return GetFormattedPrincipal(AcsPrincipalName, new Uri(GetAcsGlobalEndpointUrl()).Host, realm);
        }

        private static string GetAcsGlobalEndpointUrl()
        {
            return String.Format(CultureInfo.InvariantCulture, "https://{0}.{1}/", GlobalEndPointPrefix, AcsHostUrl);
        }

        private static JwtSecurityTokenHandler CreateJwtSecurityTokenHandler()
        {
            return new JwtSecurityTokenHandler();
        }

        private static string GetS2SAccessTokenWithClaims(
            string targetApplicationHostName,
            string targetRealm,
            IEnumerable<Claim> claims)
        {
            return IssueToken(
                ClientId,
                IssuerId,
                targetRealm,
                SharePointPrincipal,
                targetRealm,
                targetApplicationHostName,
                true,
                claims,
                claims == null);
        }

        private static Claim[] GetClaimsWithWindowsIdentity(WindowsIdentity identity)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(NameIdentifierClaimType, identity.User.Value.ToLower()),
                new Claim("nii", "urn:office:idp:activedirectory")
            };
            return claims;
        }

        private static string IssueToken(
            string sourceApplication,
            string issuerApplication,
            string sourceRealm,
            string targetApplication,
            string targetRealm,
            string targetApplicationHostName,
            bool trustedForDelegation,
            IEnumerable<Claim> claims,
            bool appOnly = false)
        {
            if (null == SigningCredentials)
            {
                throw new InvalidOperationException("SigningCredentials was not initialized");
            }

            #region Token de ator

            string issuer = string.IsNullOrEmpty(sourceRealm) ? issuerApplication : string.Format("{0}@{1}", issuerApplication, sourceRealm);
            string nameid = string.IsNullOrEmpty(sourceRealm) ? sourceApplication : string.Format("{0}@{1}", sourceApplication, sourceRealm);
            string audience = string.Format("{0}/{1}@{2}", targetApplication, targetApplicationHostName, targetRealm);

            List<Claim> actorClaims = new List<Claim>();
            actorClaims.Add(new Claim(NameIdentifierClaimType, nameid));
            if (trustedForDelegation && !appOnly)
            {
                actorClaims.Add(new Claim(TrustedForImpersonationClaimType, "true"));
            }

            // Criar Token
            JwtSecurityToken actorToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: actorClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(HighTrustAccessTokenLifetime),
                signingCredentials: SigningCredentials
                );

            string actorTokenString = new JwtSecurityTokenHandler().WriteToken(actorToken);

            if (appOnly)
            {
                // O token somente de aplicativo é o mesmo que o token de ator para o caso delegado
                return actorTokenString;
            }

            #endregion Actor token

            #region Token externo

            List<Claim> outerClaims = null == claims ? new List<Claim>() : new List<Claim>(claims);
            outerClaims.Add(new Claim(ActorTokenClaimType, actorTokenString));

            JwtSecurityToken jsonToken = new JwtSecurityToken(
                nameid, // o emissor do token externo deve corresponder à nameid do token de ator
                audience,
                outerClaims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(HighTrustAccessTokenLifetime)
                );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jsonToken);

            #endregion Outer token

            return accessToken;
        }

        #endregion

        #region AcsMetadataParser

        // Essa classe é usada para obter o documento MetaData do ponto de extremidade do STS global. Ela contém
        // métodos para analisar o documento MetaData e obter pontos de extremidade e certificado STS.
        public static class AcsMetadataParser
        {
            public static X509Certificate2 GetAcsSigningCert(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                if (null != document.keys && document.keys.Count > 0)
                {
                    JsonKey signingKey = document.keys[0];

                    if (null != signingKey && null != signingKey.keyValue)
                    {
                        return new X509Certificate2(Encoding.UTF8.GetBytes(signingKey.keyValue.value));
                    }
                }

                throw new Exception("Metadata document does not contain ACS signing certificate.");
            }

            public static string GetDelegationServiceUrl(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                JsonEndpoint delegationEndpoint = document.endpoints.SingleOrDefault(e => e.protocol == DelegationIssuance);

                if (null != delegationEndpoint)
                {
                    return delegationEndpoint.location;
                }
                throw new Exception("Metadata document does not contain Delegation Service endpoint Url");
            }

            private static JsonMetadataDocument GetMetadataDocument(string realm)
            {
                string acsMetadataEndpointUrlWithRealm = String.Format(CultureInfo.InvariantCulture, "{0}?realm={1}",
                                                                       GetAcsMetadataEndpointUrl(),
                                                                       realm);
                byte[] acsMetadata;
                using (WebClient webClient = new WebClient())
                {

                    acsMetadata = webClient.DownloadData(acsMetadataEndpointUrlWithRealm);
                }
                string jsonResponseString = Encoding.UTF8.GetString(acsMetadata);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                JsonMetadataDocument document = serializer.Deserialize<JsonMetadataDocument>(jsonResponseString);

                if (null == document)
                {
                    throw new Exception("No metadata document found at the global endpoint " + acsMetadataEndpointUrlWithRealm);
                }

                return document;
            }

            public static string GetStsUrl(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                JsonEndpoint s2sEndpoint = document.endpoints.SingleOrDefault(e => e.protocol == S2SProtocol);

                if (null != s2sEndpoint)
                {
                    return s2sEndpoint.location;
                }

                throw new Exception("Metadata document does not contain STS endpoint url");
            }

            private class JsonMetadataDocument
            {
                public string serviceName { get; set; }
                public List<JsonEndpoint> endpoints { get; set; }
                public List<JsonKey> keys { get; set; }
            }

            private class JsonEndpoint
            {
                public string location { get; set; }
                public string protocol { get; set; }
                public string usage { get; set; }
            }

            private class JsonKeyValue
            {
                public string type { get; set; }
                public string value { get; set; }
            }

            private class JsonKey
            {
                public string usage { get; set; }
                public JsonKeyValue keyValue { get; set; }
            }
        }

        #endregion
    }

    /// <summary>
    /// Um JwtSecurityToken gerado pelo SharePoint para autenticar em um aplicativo de terceiros e permitir retornos de chamada usando um token de atualização
    /// </summary>
    public class SharePointContextToken : JwtSecurityToken
    {
        public static SharePointContextToken Create(JwtSecurityToken contextToken)
        {
            return new SharePointContextToken(contextToken.Issuer, contextToken.Audiences.FirstOrDefault<string>(), contextToken.ValidFrom, contextToken.ValidTo, contextToken.Claims);
        }

        public SharePointContextToken(string issuer, string audience, DateTime validFrom, DateTime validTo, IEnumerable<Claim> claims)
        : base(issuer, audience, claims, validFrom, validTo)
        {
        }

        public SharePointContextToken(string issuer, string audience, DateTime validFrom, DateTime validTo, IEnumerable<Claim> claims, SecurityToken issuerToken, JwtSecurityToken actorToken)
            : base(issuer, audience, claims, validFrom, validTo, actorToken.SigningCredentials)
        {
            // Este método é fornecido para manter a compatibilidade com as versões anteriores do TokenHelper.
            // A versão atual do JwtSecurityToken não tem um construtor que use todos os parâmetros acima.
        }

        public SharePointContextToken(string issuer, string audience, DateTime validFrom, DateTime validTo, IEnumerable<Claim> claims, SigningCredentials signingCredentials)
            : base(issuer, audience, claims, validFrom, validTo, signingCredentials)
        {
        }

        public string NameId
        {
            get
            {
                return GetClaimValue(this, "nameid");
            }
        }

        /// <summary>
        /// A parte do nome da entidade da declaração de "appctxsender" do token de contexto
        /// </summary>
        public string TargetPrincipalName
        {
            get
            {
                string appctxsender = GetClaimValue(this, "appctxsender");

                if (appctxsender == null)
                {
                    return null;
                }

                return appctxsender.Split('@')[0];
            }
        }

        /// <summary>
        /// A declaração "refreshtoken" do token de contexto
        /// </summary>
        public string RefreshToken
        {
            get
            {
                return GetClaimValue(this, "refreshtoken");
            }
        }

        /// <summary>
        /// A declaração "CacheKey" do token de contexto
        /// </summary>
        public string CacheKey
        {
            get
            {
                string appctx = GetClaimValue(this, "appctx");
                if (appctx == null)
                {
                    return null;
                }

                ClientContext ctx = new ClientContext("http://tempuri.org");
                Dictionary<string, object> dict = (Dictionary<string, object>)ctx.ParseObjectFromJsonString(appctx);
                string cacheKey = (string)dict["CacheKey"];

                return cacheKey;
            }
        }

        /// <summary>
        /// A declaração "SecurityTokenServiceUri" do token de contexto
        /// </summary>
        public string SecurityTokenServiceUri
        {
            get
            {
                string appctx = GetClaimValue(this, "appctx");
                if (appctx == null)
                {
                    return null;
                }

                ClientContext ctx = new ClientContext("http://tempuri.org");
                Dictionary<string, object> dict = (Dictionary<string, object>)ctx.ParseObjectFromJsonString(appctx);
                string securityTokenServiceUri = (string)dict["SecurityTokenServiceUri"];

                return securityTokenServiceUri;
            }
        }

        /// <summary>
        /// A parte da realm da declaração de "audience" do token de contexto
        /// </summary>
        public string Realm
        {
            get
            {
                // Obter o primeiro realm não nulo
                foreach (string aud in Audiences)
                {
                    string tokenRealm = aud.Substring(aud.IndexOf('@') + 1);

                    if (string.IsNullOrEmpty(tokenRealm))
                    {
                        continue;
                    }
                    else
                    {
                        return tokenRealm;
                    }
                }

                return null;
            }
        }

        private static string GetClaimValue(JwtSecurityToken token, string claimType)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            foreach (Claim claim in token.Claims)
            {
                if (StringComparer.Ordinal.Equals(claim.Type, claimType))
                {
                    return claim.Value;
                }
            }

            return null;
        }

    }

    /// <summary>
    /// Representa um token de segurança que contém várias chaves de segurança geradas usando algoritmos simétricos.
    /// </summary>
    public class MultipleSymmetricKeySecurityToken : SecurityToken
    {
        /// <summary>
        /// Inicializa uma nova instância da classe MultipleSymmetricKeySecurityToken.
        /// </summary>
        /// <param name="keys">Uma enumeração das matrizes de bytes que contêm as chaves simétricas.</param>
        public MultipleSymmetricKeySecurityToken(IEnumerable<byte[]> keys)
            : this(Microsoft.IdentityModel.Tokens.UniqueId.CreateUniqueId(), keys)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe MultipleSymmetricKeySecurityToken.
        /// </summary>
        /// <param name="tokenId">O identificador exclusivo do token de segurança.</param>
        /// <param name="keys">Uma enumeração das matrizes de bytes que contêm as chaves simétricas.</param>
        public MultipleSymmetricKeySecurityToken(string tokenId, IEnumerable<byte[]> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException("keys");
            }

            if (String.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentException("Value cannot be a null or empty string.", "tokenId");
            }

            foreach (byte[] key in keys)
            {
                if (key.Length <= 0)
                {
                    throw new ArgumentException("The key length must be greater then zero.", "keys");
                }
            }

            id = tokenId;
            effectiveTime = DateTime.UtcNow;
            securityKeys = CreateSymmetricSecurityKeys(keys);
        }

        /// <summary>
        /// Obtém o identificador exclusivo do token de segurança.
        /// </summary>
        public override string Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Obtém as chaves de criptografia associadas com o token de segurança.
        /// </summary>
        public override ReadOnlyCollection<SecurityKey> SecurityKeys
        {
            get
            {
                return securityKeys.AsReadOnly();
            }
        }

        /// <summary>
        /// Obtém o primeiro instante no tempo em que esse token de segurança é válido.
        /// </summary>
        public override DateTime ValidFrom
        {
            get
            {
                return effectiveTime;
            }
        }

        /// <summary>
        /// Obtém o último instante no tempo em que esse token de segurança é válido.
        /// </summary>
        public override DateTime ValidTo
        {
            get
            {
                // Nunca expirar
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Retorna um valor que indica se o identificador de chave para esta instância pode ser resolvido para o identificador de chave especificado.
        /// </summary>
        /// <param name="keyIdentifierClause">Uma SecurityKeyIdentifierClause para comparar a esta instância</param>
        /// <returns>true se keyIdentifierClause for uma SecurityKeyIdentifierClause e se tiver o mesmo identificador exclusivo que a propriedade Id; caso contrário, false.</returns>
        public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
        {
            if (keyIdentifierClause == null)
            {
                throw new ArgumentNullException("keyIdentifierClause");
            }

            return base.MatchesKeyIdentifierClause(keyIdentifierClause);
        }

        #region membros privados

        private List<SecurityKey> CreateSymmetricSecurityKeys(IEnumerable<byte[]> keys)
        {
            List<SecurityKey> symmetricKeys = new List<SecurityKey>();
            foreach (byte[] key in keys)
            {
                symmetricKeys.Add(new InMemorySymmetricSecurityKey(key));
            }
            return symmetricKeys;
        }

        private string id;
        private DateTime effectiveTime;
        private List<SecurityKey> securityKeys;

        #endregion
    }

    /// <summary>
    /// Representa uma resposta OAuth de uma chamada ao servidor ACS.
    /// </summary>
    public class OAuthTokenResponse
    {
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public OAuthTokenResponse() { }

        /// <summary>
        /// Constrói um objeto OAuthTokenResponse usando uma matriz de bytes retornada do servidor ACS.
        /// </summary>
        /// <param name="response">A matriz de bytes bruta obtida do ACS.</param>
        public OAuthTokenResponse(byte[] response)
        {
            var serializer = new JavaScriptSerializer();
            this.Data = serializer.DeserializeObject(Encoding.UTF8.GetString(response)) as Dictionary<string, object>;

            this.AccessToken = this.GetValue("access_token");
            this.TokenType = this.GetValue("token_type");
            this.Resource = this.GetValue("resource");
            this.UserType = this.GetValue("user_type");

            long epochTime = 0;
            if (long.TryParse(this.GetValue("expires_in"), out epochTime))
            {
                this.ExpiresIn = epochTime;
            }
            if (long.TryParse(this.GetValue("expires_on"), out epochTime))
            {
                this.ExpiresOn = epochTime;
            }
            if (long.TryParse(this.GetValue("not_before"), out epochTime))
            {
                this.NotBefore = epochTime;
            }
            if (long.TryParse(this.GetValue("extended_expires_in"), out epochTime))
            {
                this.ExtendedExpiresIn = epochTime;
            }
        }

        /// <summary>
        /// Obtém o token de acesso.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Obtém os dados analisados da resposta bruta.
        /// </summary>
        public IDictionary<string, object> Data { get; }

        /// <summary>
        /// Obtém o horário da Época de expires in.
        /// </summary>
        public long ExpiresIn { get; }

        /// <summary>
        /// Obtém as expirações no tempo da Época.
        /// </summary>
        public long ExpiresOn { get; }

        /// <summary>
        /// Obtém o estendido, que expira no horário da Época.
        /// </summary>
        public long ExtendedExpiresIn { get; }

        /// <summary>
        /// Obtém as expirações que não estão antes do tempo da Época.
        /// </summary>
        public long NotBefore { get; }

        /// <summary>
        /// Obtém o recurso.
        /// </summary>
        public string Resource { get; }

        /// <summary>
        /// Obtém o tipo de token.
        /// </summary>
        public string TokenType { get; }

        /// <summary>
        /// Obtém o tipo de usuário.
        /// </summary>
        public string UserType { get; }

        /// <summary>
        /// Obtém um valor dos Dados pela chave.
        /// </summary>
        /// <param name="key">A chave.</param>
        /// <returns>O valor da chave, se existir, caso contrário, uma cadeia de caracteres vazia.</returns>
        private string GetValue(string key)
        {
            if (this.Data.TryGetValue(key, out object value))
            {
                return value as string;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// Representa um cliente Web para fazer uma chamada OAuth ao servidor ACS.
    /// </summary>
    public class OAuthClient
    {
        /// <summary>
        /// Obtém um OAuthTokenResponse com um token de atualização.
        /// </summary>
        /// <param name="uri">O URI do servidor ACS.</param>
        /// <param name="clientId">ID do cliente.</param>
        /// <param name="ClientSecret">Segredo do cliente.</param>
        /// <param name="refreshToken">Token de atualização.</param>
        /// <param name="resource">Recurso.</param>
        /// <returns>Resposta do servidor ACS.</returns>
        public static OAuthTokenResponse GetAccessTokenWithRefreshToken(string uri, string clientId,
            string ClientSecret, string refreshToken, string resource)
        {
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection
            {
                { "grant_type", "refresh_token" },
                { "client_id", clientId },
                { "client_secret", ClientSecret },
                { "refresh_token", refreshToken },
                { "resource", resource }
            };

            byte[] response = client.UploadValues(uri, "POST", values);

            return new OAuthTokenResponse(response);
        }

        /// <summary>
        /// Obtém um OAuthTokenResponse com as credenciais do cliente.
        /// </summary>
        /// <param name="uri">O URI do servidor ACS.</param>
        /// <param name="clientId">ID do cliente.</param>
        /// <param name="ClientSecret">Segredo do cliente.</param>
        /// <param name="resource">Recurso.</param>
        /// <returns>Resposta do servidor ACS.</returns>
        public static OAuthTokenResponse GetAccessTokenWithClientCredentials(string uri, string clientId,
            string ClientSecret, string resource)
        {
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", ClientSecret },
                { "resource", resource }
            };

            byte[] response = client.UploadValues(uri, "POST", values);

            return new OAuthTokenResponse(response);
        }

        /// <summary>
        /// Obtém um OAuthTokenResponse com um código de autorização.
        /// </summary>
        /// <param name="uri">O URI do servidor ACS.</param>
        /// <param name="clientId">ID do cliente.</param>
        /// <param name="ClientSecret">Segredo do cliente.</param>
        /// <param name="authorizationCode">Código de autorização.</param>
        /// <param name="redirectUri">URI de redirecionamento.</param>
        /// <param name="resource">Recurso.</param>
        /// <returns>Resposta do servidor ACS.</returns>
        public static OAuthTokenResponse GetAccessTokenWithAuthorizationCode(string uri, string clientId,
            string ClientSecret, string authorizationCode, string redirectUri, string resource)
        {
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection
            {
                { "grant_type", "authorization_code" },
                { "client_id", clientId },
                { "client_secret", ClientSecret },
                { "code", authorizationCode },
                { "redirect_uri", redirectUri },
                { "resource", resource }
            };

            byte[] response = client.UploadValues(uri, "POST", values);

            return new OAuthTokenResponse(response);
        }
    }
}
