using Microsoft.SharePoint.Client;
using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Principal;
using System.Web;

namespace SharePointAddInTestWeb
{
    /// <summary>
    /// Encapsula todas as informações do SharePoint.
    /// </summary>
    public abstract class SharePointContext
    {
        public const string SPHostUrlKey = "SPHostUrl";
        public const string SPAppWebUrlKey = "SPAppWebUrl";
        public const string SPLanguageKey = "SPLanguage";
        public const string SPClientTagKey = "SPClientTag";
        public const string SPProductNumberKey = "SPProductNumber";

        protected static readonly long AccessTokenLifetimeTolerance = 5 * 60; // 5 minutos

        private readonly Uri spHostUrl;
        private readonly Uri spAppWebUrl;
        private readonly string spLanguage;
        private readonly string spClientTag;
        private readonly string spProductNumber;

        // <AccessTokenString, UtcExpiresOn no horário da Época>
        protected Tuple<string, long> userAccessTokenForSPHost;
        protected Tuple<string, long> userAccessTokenForSPAppWeb;
        protected Tuple<string, long> appOnlyAccessTokenForSPHost;
        protected Tuple<string, long> appOnlyAccessTokenForSPAppWeb;

        /// <summary>
        /// Obtém a URL do host do SharePoint da QueryString da solicitação HTTP especificada.
        /// </summary>
        /// <param name="httpRequest">A solicitação HTTP especificada.</param>
        /// <returns>A URL do host do SharePoint. Retornará <c>null</c> se a solicitação HTTP não contiver a URL do host do SharePoint.</returns>
        public static Uri GetSPHostUrl(HttpRequestBase httpRequest)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException("httpRequest");
            }

            string spHostUrlString = TokenHelper.EnsureTrailingSlash(httpRequest.QueryString[SPHostUrlKey]);

            if (Uri.TryCreate(spHostUrlString, UriKind.Absolute, out Uri spHostUrl) &&
                (spHostUrl.Scheme == Uri.UriSchemeHttp || spHostUrl.Scheme == Uri.UriSchemeHttps))
            {
                return spHostUrl;
            }

            return null;
        }

        /// <summary>
        /// Obtém a URL do host do SharePoint da QueryString da solicitação HTTP especificada.
        /// </summary>
        /// <param name="httpRequest">A solicitação HTTP especificada.</param>
        /// <returns>A URL do host do SharePoint. Retornará <c>null</c> se a solicitação HTTP não contiver a URL do host do SharePoint.</returns>
        public static Uri GetSPHostUrl(HttpRequest httpRequest)
        {
            return GetSPHostUrl(new HttpRequestWrapper(httpRequest));
        }

        /// <summary>
        /// A URL do host do SharePoint.
        /// </summary>
        public Uri SPHostUrl
        {
            get { return this.spHostUrl; }
        }

        /// <summary>
        /// A URL da Web do aplicativo SharePoint.
        /// </summary>
        public Uri SPAppWebUrl
        {
            get { return this.spAppWebUrl; }
        }

        /// <summary>
        /// O idioma do SharePoint.
        /// </summary>
        public string SPLanguage
        {
            get { return this.spLanguage; }
        }

        /// <summary>
        /// A marca de cliente do SharePoint.
        /// </summary>
        public string SPClientTag
        {
            get { return this.spClientTag; }
        }

        /// <summary>
        /// O número do produto do SharePoint.
        /// </summary>
        public string SPProductNumber
        {
            get { return this.spProductNumber; }
        }

        /// <summary>
        /// O token de acesso do usuário para o host do SharePoint.
        /// </summary>
        public abstract string UserAccessTokenForSPHost
        {
            get;
        }

        /// <summary>
        /// O token de acesso do usuário para o aplicativo Web do SharePoint.
        /// </summary>
        public abstract string UserAccessTokenForSPAppWeb
        {
            get;
        }

        /// <summary>
        /// O token de acesso somente do aplicativo para o host do SharePoint.
        /// </summary>
        public abstract string AppOnlyAccessTokenForSPHost
        {
            get;
        }

        /// <summary>
        /// O token de acesso somente do aplicativo para o aplicativo Web do SharePoint.
        /// </summary>
        public abstract string AppOnlyAccessTokenForSPAppWeb
        {
            get;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="spHostUrl">A URL do host do SharePoint.</param>
        /// <param name="spAppWebUrl">A URL da Web do aplicativo SharePoint.</param>
        /// <param name="spLanguage">O idioma do SharePoint.</param>
        /// <param name="spClientTag">A marca de cliente do SharePoint.</param>
        /// <param name="spProductNumber">O número do produto do SharePoint.</param>
        protected SharePointContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber)
        {
            if (spHostUrl == null)
            {
                throw new ArgumentNullException("spHostUrl");
            }

            if (string.IsNullOrEmpty(spLanguage))
            {
                throw new ArgumentNullException("spLanguage");
            }

            if (string.IsNullOrEmpty(spClientTag))
            {
                throw new ArgumentNullException("spClientTag");
            }

            if (string.IsNullOrEmpty(spProductNumber))
            {
                throw new ArgumentNullException("spProductNumber");
            }

            this.spHostUrl = spHostUrl;
            this.spAppWebUrl = spAppWebUrl;
            this.spLanguage = spLanguage;
            this.spClientTag = spClientTag;
            this.spProductNumber = spProductNumber;
        }

        /// <summary>
        /// Cria um ClientContext do usuário para o host do SharePoint.
        /// </summary>
        /// <returns>Uma instância de ClientContext.</returns>
        public ClientContext CreateUserClientContextForSPHost()
        {
            return CreateClientContext(this.SPHostUrl, this.UserAccessTokenForSPHost);
        }

        /// <summary>
        /// Cria um ClientContext do usuário para o aplicativo Web do SharePoint.
        /// </summary>
        /// <returns>Uma instância de ClientContext.</returns>
        public ClientContext CreateUserClientContextForSPAppWeb()
        {
            return CreateClientContext(this.SPAppWebUrl, this.UserAccessTokenForSPAppWeb);
        }

        /// <summary>
        /// Cria um ClientContext somente do aplicativo para o host do SharePoint.
        /// </summary>
        /// <returns>Uma instância de ClientContext.</returns>
        public ClientContext CreateAppOnlyClientContextForSPHost()
        {
            return CreateClientContext(this.SPHostUrl, this.AppOnlyAccessTokenForSPHost);
        }

        /// <summary>
        /// Cria um ClientContext somente do aplicativo para o aplicativo Web do SharePoint.
        /// </summary>
        /// <returns>Uma instância de ClientContext.</returns>
        public ClientContext CreateAppOnlyClientContextForSPAppWeb()
        {
            return CreateClientContext(this.SPAppWebUrl, this.AppOnlyAccessTokenForSPAppWeb);
        }

        /// <summary>
        /// Obtém a cadeia de conexão de banco de dados do SharePoint para o suplemento auto-hospedado.
        /// Esse método é preterido porque a opção auto-hospedado não está mais disponível.
        /// </summary>
        [ObsoleteAttribute("This method is deprecated because the autohosted option is no longer available.", true)]
        public string GetDatabaseConnectionString()
        {
            throw new NotSupportedException("This method is deprecated because the autohosted option is no longer available.");
        }

        /// <summary>
        /// Determina se o token de acesso especificado é válido.
        /// Ele considera um token de acesso como não é válido se ele for nulo ou tiver expirado.
        /// </summary>
        /// <param name="accessToken">O token de acesso a verificar.</param>
        /// <returns>True se o token de acesso for válido.</returns>
        protected static bool IsAccessTokenValid(Tuple<string, long> accessToken)
        {
            return accessToken != null &&
                   !string.IsNullOrEmpty(accessToken.Item1) &&
                   accessToken.Item2 > TokenHelper.EpochTimeNow();
        }

        /// <summary>
        /// Cria um ClientContext com a URL do site do SharePoint especificada e o token de acesso.
        /// </summary>
        /// <param name="spSiteUrl">a URL do site.</param>
        /// <param name="accessToken">O token de acesso.</param>
        /// <returns>Uma instância de ClientContext.</returns>
        private static ClientContext CreateClientContext(Uri spSiteUrl, string accessToken)
        {
            if (spSiteUrl != null && !string.IsNullOrEmpty(accessToken))
            {
                return TokenHelper.GetClientContextWithAccessToken(spSiteUrl.AbsoluteUri, accessToken);
            }

            return null;
        }
    }

    /// <summary>
    /// Status de redirecionamento.
    /// </summary>
    public enum RedirectionStatus
    {
        Ok,
        ShouldRedirect,
        CanNotRedirect
    }

    /// <summary>
    /// Fornece instâncias de SharePointContext.
    /// </summary>
    public abstract class SharePointContextProvider
    {
        private static SharePointContextProvider current;

        /// <summary>
        /// A instância de SharePointContextProvider atual.
        /// </summary>
        public static SharePointContextProvider Current
        {
            get { return SharePointContextProvider.current; }
        }

        /// <summary>
        /// Inicializa a instância de SharePointContextProvider padrão.
        /// </summary>
        static SharePointContextProvider()
        {
            if (!TokenHelper.IsHighTrustApp())
            {
                SharePointContextProvider.current = new SharePointAcsContextProvider();
            }
            else
            {
                SharePointContextProvider.current = new SharePointHighTrustContextProvider();
            }
        }

        /// <summary>
        /// Registra a instância SharePointContextProvider especificada como atual.
        /// Ela deve ser chamada por Application_Start() em Global.asax.
        /// </summary>
        /// <param name="provider">O SharePointContextProvider a definir como atual.</param>
        public static void Register(SharePointContextProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            SharePointContextProvider.current = provider;
        }

        /// <summary>
        /// Verifica se é necessário redirecionar para o SharePoint para que o usuário se autentique.
        /// </summary>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <param name="redirectUrl">A URL de redirecionamento para o SharePoint se o status for ShouldRedirect. <c>Nulo</c> se o status for Ok ou CanNotRedirect.</param>
        /// <returns>Status de redirecionamento.</returns>
        public static RedirectionStatus CheckRedirectionStatus(HttpContextBase httpContext, out Uri redirectUrl)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            redirectUrl = null;
            bool contextTokenExpired = false;

            try
            {
                if (SharePointContextProvider.Current.GetSharePointContext(httpContext) != null)
                {
                    return RedirectionStatus.Ok;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                contextTokenExpired = true;
            }

            const string SPHasRedirectedToSharePointKey = "SPHasRedirectedToSharePoint";

            if (!string.IsNullOrEmpty(httpContext.Request.QueryString[SPHasRedirectedToSharePointKey]) && !contextTokenExpired)
            {
                return RedirectionStatus.CanNotRedirect;
            }

            Uri spHostUrl = SharePointContext.GetSPHostUrl(httpContext.Request);

            if (spHostUrl == null)
            {
                return RedirectionStatus.CanNotRedirect;
            }

            if (StringComparer.OrdinalIgnoreCase.Equals(httpContext.Request.HttpMethod, "POST"))
            {
                return RedirectionStatus.CanNotRedirect;
            }

            Uri requestUrl = httpContext.Request.Url;

            var queryNameValueCollection = HttpUtility.ParseQueryString(requestUrl.Query);

            // Remove os valores que estão incluídos em {StandardTokens}, pois {StandardTokens} será inserido no início da cadeia de caracteres de consulta.
            queryNameValueCollection.Remove(SharePointContext.SPHostUrlKey);
            queryNameValueCollection.Remove(SharePointContext.SPAppWebUrlKey);
            queryNameValueCollection.Remove(SharePointContext.SPLanguageKey);
            queryNameValueCollection.Remove(SharePointContext.SPClientTagKey);
            queryNameValueCollection.Remove(SharePointContext.SPProductNumberKey);

            // Adiciona SPHasRedirectedToSharePoint=1.
            queryNameValueCollection.Add(SPHasRedirectedToSharePointKey, "1");

            UriBuilder returnUrlBuilder = new UriBuilder(requestUrl);
            returnUrlBuilder.Query = queryNameValueCollection.ToString();

            // Insere StandardTokens.
            const string StandardTokens = "{StandardTokens}";
            string returnUrlString = returnUrlBuilder.Uri.AbsoluteUri;
            returnUrlString = returnUrlString.Insert(returnUrlString.IndexOf("?") + 1, StandardTokens + "&");

            // Constrói a URL de redirecionamento.
            string redirectUrlString = TokenHelper.GetAppContextTokenRequestUrl(spHostUrl.AbsoluteUri, Uri.EscapeDataString(returnUrlString));

            redirectUrl = new Uri(redirectUrlString, UriKind.Absolute);

            return RedirectionStatus.ShouldRedirect;
        }

        /// <summary>
        /// Verifica se é necessário redirecionar para o SharePoint para que o usuário se autentique.
        /// </summary>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <param name="redirectUrl">A URL de redirecionamento para o SharePoint se o status for ShouldRedirect. <c>Nulo</c> se o status for Ok ou CanNotRedirect.</param>
        /// <returns>Status de redirecionamento.</returns>
        public static RedirectionStatus CheckRedirectionStatus(HttpContext httpContext, out Uri redirectUrl)
        {
            return CheckRedirectionStatus(new HttpContextWrapper(httpContext), out redirectUrl);
        }

        /// <summary>
        /// Cria uma instância do SharePointContext com a solicitação HTTP especificada.
        /// </summary>
        /// <param name="httpRequest">A solicitação HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se ocorrerem erros.</returns>
        public SharePointContext CreateSharePointContext(HttpRequestBase httpRequest)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException("httpRequest");
            }

            // SPHostUrl
            Uri spHostUrl = SharePointContext.GetSPHostUrl(httpRequest);
            if (spHostUrl == null)
            {
                return null;
            }

            // SPAppWebUrl
            string spAppWebUrlString = TokenHelper.EnsureTrailingSlash(httpRequest.QueryString[SharePointContext.SPAppWebUrlKey]);
            Uri spAppWebUrl;
            if (!Uri.TryCreate(spAppWebUrlString, UriKind.Absolute, out spAppWebUrl) ||
                !(spAppWebUrl.Scheme == Uri.UriSchemeHttp || spAppWebUrl.Scheme == Uri.UriSchemeHttps))
            {
                spAppWebUrl = null;
            }

            // SPLanguage
            string spLanguage = httpRequest.QueryString[SharePointContext.SPLanguageKey];
            if (string.IsNullOrEmpty(spLanguage))
            {
                return null;
            }

            // SPClientTag
            string spClientTag = httpRequest.QueryString[SharePointContext.SPClientTagKey];
            if (string.IsNullOrEmpty(spClientTag))
            {
                return null;
            }

            // SPProductNumber
            string spProductNumber = httpRequest.QueryString[SharePointContext.SPProductNumberKey];
            if (string.IsNullOrEmpty(spProductNumber))
            {
                return null;
            }

            return CreateSharePointContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, httpRequest);
        }

        /// <summary>
        /// Cria uma instância do SharePointContext com a solicitação HTTP especificada.
        /// </summary>
        /// <param name="httpRequest">A solicitação HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se ocorrerem erros.</returns>
        public SharePointContext CreateSharePointContext(HttpRequest httpRequest)
        {
            return CreateSharePointContext(new HttpRequestWrapper(httpRequest));
        }

        /// <summary>
        /// Obtém uma instância do SharePointContext associada ao contexto HTTP especificado.
        /// </summary>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se não for encontrada e uma nova instância não puder ser criada.</returns>
        public SharePointContext GetSharePointContext(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            Uri spHostUrl = SharePointContext.GetSPHostUrl(httpContext.Request);
            if (spHostUrl == null)
            {
                return null;
            }

            SharePointContext spContext = LoadSharePointContext(httpContext);

            if (spContext == null || !ValidateSharePointContext(spContext, httpContext))
            {
                spContext = CreateSharePointContext(httpContext.Request);

                if (spContext != null)
                {
                    SaveSharePointContext(spContext, httpContext);
                }
            }

            return spContext;
        }

        /// <summary>
        /// Obtém uma instância do SharePointContext associada ao contexto HTTP especificado.
        /// </summary>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se não for encontrada e uma nova instância não puder ser criada.</returns>
        public SharePointContext GetSharePointContext(HttpContext httpContext)
        {
            return GetSharePointContext(new HttpContextWrapper(httpContext));
        }

        /// <summary>
        /// Cria uma instância de SharePointContext.
        /// </summary>
        /// <param name="spHostUrl">A URL do host do SharePoint.</param>
        /// <param name="spAppWebUrl">A URL da Web do aplicativo SharePoint.</param>
        /// <param name="spLanguage">O idioma do SharePoint.</param>
        /// <param name="spClientTag">A marca de cliente do SharePoint.</param>
        /// <param name="spProductNumber">O número do produto do SharePoint.</param>
        /// <param name="httpRequest">A solicitação HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se ocorrerem erros.</returns>
        protected abstract SharePointContext CreateSharePointContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber, HttpRequestBase httpRequest);

        /// <summary>
        /// Valida se o SharePointContext fornecido pode ser usado com o contexto HTTP especificado.
        /// </summary>
        /// <param name="spContext">O SharePointContext.</param>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <returns>True se o SharePointContext fornecido puder ser usado com o contexto HTTP especificado.</returns>
        protected abstract bool ValidateSharePointContext(SharePointContext spContext, HttpContextBase httpContext);

        /// <summary>
        /// Carrega a instância do SharePointContext associada ao contexto HTTP especificado.
        /// </summary>
        /// <param name="httpContext">O contexto HTTP.</param>
        /// <returns>A instância de SharePointContext. Retornará <c>nulo</c> se ela não for encontrada.</returns>
        protected abstract SharePointContext LoadSharePointContext(HttpContextBase httpContext);

        /// <summary>
        /// Salva a instância do SharePointContext especificada associada ao contexto HTTP especificado.
        /// <c>nulo</c> é aceito para limpar a instância do SharePointContext associada ao contexto HTTP especificado.
        /// </summary>
        /// <param name="spContext">A instância de SharePointContext a ser salva ou <c>nulo</c>.</param>
        /// <param name="httpContext">O contexto HTTP.</param>
        protected abstract void SaveSharePointContext(SharePointContext spContext, HttpContextBase httpContext);
    }

    #region ACS

    /// <summary>
    /// Encapsula todas as informações do SharePoint no modo ACS.
    /// </summary>
    public class SharePointAcsContext : SharePointContext
    {
        private readonly string contextToken;
        private readonly SharePointContextToken contextTokenObj;

        /// <summary>
        /// O token de contexto.
        /// </summary>
        public string ContextToken
        {
            get { return this.contextTokenObj.ValidTo > DateTime.UtcNow ? this.contextToken : null; }
        }

        /// <summary>
        /// A declaração "CacheKey" do token de contexto.
        /// </summary>
        public string CacheKey
        {
            get { return this.contextTokenObj.ValidTo > DateTime.UtcNow ? this.contextTokenObj.CacheKey : null; }
        }

        /// <summary>
        /// A declaração "refreshtoken" do token de contexto.
        /// </summary>
        public string RefreshToken
        {
            get { return this.contextTokenObj.ValidTo > DateTime.UtcNow ? this.contextTokenObj.RefreshToken : null; }
        }

        public override string UserAccessTokenForSPHost
        {
            get
            {
                return GetAccessTokenString(ref this.userAccessTokenForSPHost,
                                            () => TokenHelper.GetAccessToken(this.contextTokenObj, this.SPHostUrl.Authority));
            }
        }

        public override string UserAccessTokenForSPAppWeb
        {
            get
            {
                if (this.SPAppWebUrl == null)
                {
                    return null;
                }

                return GetAccessTokenString(ref this.userAccessTokenForSPAppWeb,
                                            () => TokenHelper.GetAccessToken(this.contextTokenObj, this.SPAppWebUrl.Authority));
            }
        }

        public override string AppOnlyAccessTokenForSPHost
        {
            get
            {
                return GetAccessTokenString(ref this.appOnlyAccessTokenForSPHost,
                                            () => TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, this.SPHostUrl.Authority, TokenHelper.GetRealmFromTargetUrl(this.SPHostUrl)));
            }
        }

        public override string AppOnlyAccessTokenForSPAppWeb
        {
            get
            {
                if (this.SPAppWebUrl == null)
                {
                    return null;
                }

                return GetAccessTokenString(ref this.appOnlyAccessTokenForSPAppWeb,
                                            () => TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, this.SPAppWebUrl.Authority, TokenHelper.GetRealmFromTargetUrl(this.SPAppWebUrl)));
            }
        }

        public SharePointAcsContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber, string contextToken, SharePointContextToken contextTokenObj)
            : base(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber)
        {
            if (string.IsNullOrEmpty(contextToken))
            {
                throw new ArgumentNullException("contextToken");
            }

            if (contextTokenObj == null)
            {
                throw new ArgumentNullException("contextTokenObj");
            }

            this.contextToken = contextToken;
            this.contextTokenObj = contextTokenObj;
        }

        /// <summary>
        /// Garante o token de acesso é válido e o retorna.
        /// </summary>
        /// <param name="accessToken">O token de acesso a verificar.</param>
        /// <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
        /// <returns>A cadeia de caracteres do token de acesso.</returns>
        private static string GetAccessTokenString(ref Tuple<string, long> accessToken, Func<OAuthTokenResponse> tokenRenewalHandler)
        {
            RenewAccessTokenIfNeeded(ref accessToken, tokenRenewalHandler);

            return IsAccessTokenValid(accessToken) ? accessToken.Item1 : null;
        }

        /// <summary>
        /// Renova o token de acesso se ele não for válido.
        /// </summary>
        /// <param name="accessToken">O token de acesso a renovar.</param>
        /// <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
        private static void RenewAccessTokenIfNeeded(ref Tuple<string, long> accessToken, Func<OAuthTokenResponse> tokenRenewalHandler)
        {
            if (IsAccessTokenValid(accessToken))
            {
                return;
            }

            try
            {
                OAuthTokenResponse oauthTokenResponse = tokenRenewalHandler();

                long expiresOn = oauthTokenResponse.ExpiresOn;

                if ((expiresOn - oauthTokenResponse.NotBefore) > AccessTokenLifetimeTolerance)
                {
                    // Fazer com que o token de acesso seja renovado um pouco mais cedo do que a hora em que ele expirar
                    // para que as chamadas para o SharePoint com ele tenham tempo suficiente para serem concluídas com êxito.
                    expiresOn -= AccessTokenLifetimeTolerance;
                }

                accessToken = Tuple.Create(oauthTokenResponse.AccessToken, expiresOn);
            }
            catch (WebException)
            {
            }
        }
    }

    /// <summary>
    /// Provedor padrão para SharePointAcsContext.
    /// </summary>
    public class SharePointAcsContextProvider : SharePointContextProvider
    {
        private const string SPContextKey = "SPContext";
        private const string SPCacheKeyKey = "SPCacheKey";

        protected override SharePointContext CreateSharePointContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber, HttpRequestBase httpRequest)
        {
            string contextTokenString = TokenHelper.GetContextTokenFromRequest(httpRequest);
            if (string.IsNullOrEmpty(contextTokenString))
            {
                return null;
            }

            SharePointContextToken contextToken = null;
            try
            {
                contextToken = TokenHelper.ReadAndValidateContextToken(contextTokenString, httpRequest.Url.Authority);
            }
            catch (WebException)
            {
                return null;
            }
            catch (AudienceUriValidationFailedException)
            {
                return null;
            }

            return new SharePointAcsContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, contextTokenString, contextToken);
        }

        protected override bool ValidateSharePointContext(SharePointContext spContext, HttpContextBase httpContext)
        {
            SharePointAcsContext spAcsContext = spContext as SharePointAcsContext;

            if (spAcsContext != null)
            {
                Uri spHostUrl = SharePointContext.GetSPHostUrl(httpContext.Request);
                string contextToken = TokenHelper.GetContextTokenFromRequest(httpContext.Request);
                HttpCookie spCacheKeyCookie = httpContext.Request.Cookies[SPCacheKeyKey];
                string spCacheKey = spCacheKeyCookie != null ? spCacheKeyCookie.Value : null;

                return spHostUrl == spAcsContext.SPHostUrl &&
                       !string.IsNullOrEmpty(spAcsContext.CacheKey) &&
                       spCacheKey == spAcsContext.CacheKey &&
                       !string.IsNullOrEmpty(spAcsContext.ContextToken) &&
                       (string.IsNullOrEmpty(contextToken) || contextToken == spAcsContext.ContextToken);
            }

            return false;
        }

        protected override SharePointContext LoadSharePointContext(HttpContextBase httpContext)
        {
            return httpContext.Session[SPContextKey] as SharePointAcsContext;
        }

        protected override void SaveSharePointContext(SharePointContext spContext, HttpContextBase httpContext)
        {
            SharePointAcsContext spAcsContext = spContext as SharePointAcsContext;

            if (spAcsContext != null)
            {
                HttpCookie spCacheKeyCookie = new HttpCookie(SPCacheKeyKey)
                {
                    Value = spAcsContext.CacheKey,
                    Secure = true,
                    HttpOnly = true
                };

                httpContext.Response.AppendCookie(spCacheKeyCookie);
            }

            httpContext.Session[SPContextKey] = spAcsContext;
        }
    }

    #endregion ACS

    #region HighTrust

    /// <summary>
    /// Encapsula todas as informações do SharePoint no modo HighTrust.
    /// </summary>
    public class SharePointHighTrustContext : SharePointContext
    {
        private readonly WindowsIdentity logonUserIdentity;

        /// <summary>
        /// A identidade do Windows para o usuário atual.
        /// </summary>
        public WindowsIdentity LogonUserIdentity
        {
            get { return this.logonUserIdentity; }
        }

        public override string UserAccessTokenForSPHost
        {
            get
            {
                return GetAccessTokenString(ref this.userAccessTokenForSPHost,
                                            () => TokenHelper.GetS2SAccessTokenWithWindowsIdentity(this.SPHostUrl, this.LogonUserIdentity));
            }
        }

        public override string UserAccessTokenForSPAppWeb
        {
            get
            {
                if (this.SPAppWebUrl == null)
                {
                    return null;
                }

                return GetAccessTokenString(ref this.userAccessTokenForSPAppWeb,
                                            () => TokenHelper.GetS2SAccessTokenWithWindowsIdentity(this.SPAppWebUrl, this.LogonUserIdentity));
            }
        }

        public override string AppOnlyAccessTokenForSPHost
        {
            get
            {
                return GetAccessTokenString(ref this.appOnlyAccessTokenForSPHost,
                                            () => TokenHelper.GetS2SAccessTokenWithWindowsIdentity(this.SPHostUrl, null));
            }
        }

        public override string AppOnlyAccessTokenForSPAppWeb
        {
            get
            {
                if (this.SPAppWebUrl == null)
                {
                    return null;
                }

                return GetAccessTokenString(ref this.appOnlyAccessTokenForSPAppWeb,
                                            () => TokenHelper.GetS2SAccessTokenWithWindowsIdentity(this.SPAppWebUrl, null));
            }
        }

        public SharePointHighTrustContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber, WindowsIdentity logonUserIdentity)
            : base(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber)
        {
            if (logonUserIdentity == null)
            {
                throw new ArgumentNullException("logonUserIdentity");
            }

            this.logonUserIdentity = logonUserIdentity;
        }

        /// <summary>
        /// Garante o token de acesso é válido e o retorna.
        /// </summary>
        /// <param name="accessToken">O token de acesso a verificar.</param>
        /// <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
        /// <returns>A cadeia de caracteres do token de acesso.</returns>
        private static string GetAccessTokenString(ref Tuple<string, long> accessToken, Func<string> tokenRenewalHandler)
        {
            RenewAccessTokenIfNeeded(ref accessToken, tokenRenewalHandler);

            return IsAccessTokenValid(accessToken) ? accessToken.Item1 : null;
        }

        /// <summary>
        /// Renova o token de acesso se ele não for válido.
        /// </summary>
        /// <param name="accessToken">O token de acesso a renovar.</param>
        /// <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
        private static void RenewAccessTokenIfNeeded(ref Tuple<string, long> accessToken, Func<string> tokenRenewalHandler)
        {
            if (IsAccessTokenValid(accessToken))
            {
                return;
            }

            long expiresOn = TokenHelper.EpochTimeNow() + (long)TokenHelper.HighTrustAccessTokenLifetime.TotalSeconds;

            if (TokenHelper.HighTrustAccessTokenLifetime.TotalSeconds > AccessTokenLifetimeTolerance)
            {
                // Fazer com que o token de acesso seja renovado um pouco mais cedo do que a hora em que ele expirar
                // para que as chamadas para o SharePoint com ele tenham tempo suficiente para serem concluídas com êxito.
                expiresOn -= AccessTokenLifetimeTolerance;
            }

            accessToken = Tuple.Create(tokenRenewalHandler(), expiresOn);
        }
    }

    /// <summary>
    /// Provedor padrão para SharePointHighTrustContext.
    /// </summary>
    public class SharePointHighTrustContextProvider : SharePointContextProvider
    {
        private const string SPContextKey = "SPContext";

        protected override SharePointContext CreateSharePointContext(Uri spHostUrl, Uri spAppWebUrl, string spLanguage, string spClientTag, string spProductNumber, HttpRequestBase httpRequest)
        {
            WindowsIdentity logonUserIdentity = httpRequest.LogonUserIdentity;
            if (logonUserIdentity == null || !logonUserIdentity.IsAuthenticated || logonUserIdentity.IsGuest || logonUserIdentity.User == null)
            {
                return null;
            }

            return new SharePointHighTrustContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, logonUserIdentity);
        }

        protected override bool ValidateSharePointContext(SharePointContext spContext, HttpContextBase httpContext)
        {
            SharePointHighTrustContext spHighTrustContext = spContext as SharePointHighTrustContext;

            if (spHighTrustContext != null)
            {
                Uri spHostUrl = SharePointContext.GetSPHostUrl(httpContext.Request);
                WindowsIdentity logonUserIdentity = httpContext.Request.LogonUserIdentity;

                return spHostUrl == spHighTrustContext.SPHostUrl &&
                       logonUserIdentity != null &&
                       logonUserIdentity.IsAuthenticated &&
                       !logonUserIdentity.IsGuest &&
                       logonUserIdentity.User == spHighTrustContext.LogonUserIdentity.User;
            }

            return false;
        }

        protected override SharePointContext LoadSharePointContext(HttpContextBase httpContext)
        {
            return httpContext.Session[SPContextKey] as SharePointHighTrustContext;
        }

        protected override void SaveSharePointContext(SharePointContext spContext, HttpContextBase httpContext)
        {
            httpContext.Session[SPContextKey] = spContext as SharePointHighTrustContext;
        }
    }

    #endregion HighTrust
}
