Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IdentityModel.Tokens
Imports System.IdentityModel.Tokens.Jwt
Imports System.IO
Imports System.Net
Imports System.Security.Claims
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Principal
Imports System.ServiceModel
Imports System.Web.Configuration
Imports System.Web.Script.Serialization
Imports Microsoft.SharePoint.Client
Imports Microsoft.SharePoint.Client.EventReceivers
Imports SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials
Imports SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
Imports TokenValidationParameters = Microsoft.IdentityModel.Tokens.TokenValidationParameters
Imports X509SigningCredentials = Microsoft.IdentityModel.Tokens.X509SigningCredentials

Public NotInheritable Class TokenHelper

#Region "campos públicos"

    ''' <summary>
    ''' Entidade do SharePoint.
    ''' </summary>
    Public Const SharePointPrincipal As String = "00000003-0000-0ff1-ce00-000000000000"

    ''' <summary>
    ''' Tempo de vida do token de acesso HighTrust, 12 horas.
    ''' </summary>
    Public Shared ReadOnly HighTrustAccessTokenLifetime As TimeSpan = TimeSpan.FromHours(12.0)

#End Region

#Region "métodos públicos"

    ''' <summary>
    ''' Recupera a cadeia de caracteres de token de contexto da solicitação especificada procurando nomes de parâmetros conhecidos no
    ''' parâmetros de formulário via solicitação POST e a querystring. Retornará Nada se nenhum token de contexto for encontrado.
    ''' </summary>
    ''' <param name="request">HttpRequest em que procurar um token de contexto</param>
    ''' <returns>A cadeia de caracteres do token de contexto</returns>
    Public Shared Function GetContextTokenFromRequest(request As HttpRequest) As String
        Return GetContextTokenFromRequest(New HttpRequestWrapper(request))
    End Function

    ''' <summary>
    ''' Recupera a cadeia de caracteres de token de contexto da solicitação especificada procurando nomes de parâmetros conhecidos no
    ''' parâmetros de formulário via solicitação POST e a querystring. Retornará Nada se nenhum token de contexto for encontrado.
    ''' </summary>
    ''' <param name="request">HttpRequest em que procurar um token de contexto</param>
    ''' <returns>A cadeia de caracteres do token de contexto</returns>
    Public Shared Function GetContextTokenFromRequest(request As HttpRequestBase) As String
        Dim paramNames As String() = {"AppContext", "AppContextToken", "AccessToken", "SPAppToken"}
        For Each paramName As String In paramNames
            If Not String.IsNullOrEmpty(request.Form(paramName)) Then
                Return request.Form(paramName)
            End If
            If Not String.IsNullOrEmpty(request.QueryString(paramName)) Then
                Return request.QueryString(paramName)
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Validar se uma cadeia de caracteres de token de contexto especificada é destinada a este aplicativo com base nos parâmetros
    ''' especificado em web.config. Os parâmetros de web.config usados para validação incluem ClientId,
    ''' HostedAppHostNameOverride, HostedAppHostName, ClientSecret e Realm (se for especificado). Se HostedAppHostNameOverride estiver presente,
    ''' será usado para validação. Caso contrário, se o <paramref name="appHostName"/> não for
    ''' Nothing, ele será usado para validação no lugar de HostedAppHostName de web.config. Se o token for inválido, um
    ''' exceção será lançada. Se o identificador for válido, a URL de metadados STS estáticos do TokenHelper será atualizada com base no conteúdo do token
    ''' e um JwtSecurityToken com base no token de contexto é retornado.
    ''' </summary>
    ''' <param name="contextTokenString">O token de contexto a validar</param>
    ''' <param name="appHostName">A autoridade da URL, consistindo de endereço IP ou nome de host do DNS (Sistema de Nomes de Domínio) e o número da porta a usar para validação da audiência do token.
    ''' Se for Nothing, a configuração HostedAppHostName de web.config será usada em seu lugar. A configuração HostedAppHostNameOverride de web.config será usada se estiver presente
    ''' para validação no lugar de <paramref name="appHostName"/> .</param>
    ''' <returns>Um JwtSecurityToken com base no token de contexto.</returns>
    Public Shared Function ReadAndValidateContextToken(contextTokenString As String, Optional appHostName As String = Nothing) As SharePointContextToken
        Dim securityKeys As List(Of SymmetricSecurityKey) = New List(Of SymmetricSecurityKey) From {
            New SymmetricSecurityKey(Convert.FromBase64String(ClientSecret))
        }

        If Not String.IsNullOrEmpty(SecondaryClientSecret) Then
            securityKeys.Add(New SymmetricSecurityKey(Convert.FromBase64String(SecondaryClientSecret)))
        End If

        Dim tokenHandler As JwtSecurityTokenHandler = CreateJwtSecurityTokenHandler()
        Dim parameters As TokenValidationParameters = New TokenValidationParameters With {
            .ValidateIssuer = False,
            .ValidateAudience = False, ' validado abaixo
            .IssuerSigningKeys = securityKeys ' validar a assinatura
        }

        Dim securityToken As Microsoft.IdentityModel.Tokens.SecurityToken = Nothing
        tokenHandler.ValidateToken(contextTokenString, parameters, securityToken)
        Dim token As SharePointContextToken = SharePointContextToken.Create(securityToken)

        Dim stsAuthority As String = (New Uri(token.SecurityTokenServiceUri)).Authority
        Dim firstDot As Integer = stsAuthority.IndexOf("."c)

        GlobalEndPointPrefix = stsAuthority.Substring(0, firstDot)
        AcsHostUrl = stsAuthority.Substring(firstDot + 1)


        Dim acceptableAudiences As String()
        If Not [String].IsNullOrEmpty(HostedAppHostNameOverride) Then
            acceptableAudiences = HostedAppHostNameOverride.Split(";"c)
        ElseIf appHostName Is Nothing Then
            acceptableAudiences = {HostedAppHostName}
        Else
            acceptableAudiences = {appHostName}
        End If

        Dim validationSuccessful As Boolean
        Dim definedRealm As String = If(Realm, token.Realm)
        For Each audience In acceptableAudiences
            Dim principal As String = GetFormattedPrincipal(ClientId, audience, definedRealm)
            If token.Audiences.First(Function(item) StringComparer.OrdinalIgnoreCase.Equals(item, principal)) IsNot Nothing Then
                validationSuccessful = True
                Exit For
            End If
        Next

        If Not validationSuccessful Then
            Throw New AudienceUriValidationFailedException([String].Format(CultureInfo.CurrentCulture, """{0}"" is not the intended audience ""{1}""", [String].Join(";", acceptableAudiences), token.Audiences.ToArray))
        End If

        Return token
    End Function

    ''' <summary>
    ''' Recupera um token de acesso do ACS para chamar a origem do token de contexto especificado no
    ''' targetHost especificado. O targetHost deve ser registrado pela entidade que enviou o token de contexto.
    ''' </summary>
    ''' <param name="contextToken">Token de contexto emitido pelo público-alvo do token de acesso</param>
    ''' <param name="targetHost">Autoridade de URL da entidade de destino</param>
    ''' <returns>Um token de acesso com uma audiência correspondente à origem do token de contexto</returns>
    Public Shared Function GetAccessToken(contextToken As SharePointContextToken, targetHost As String) As OAuthTokenResponse

        Dim targetPrincipalName As String = contextToken.TargetPrincipalName

        ' Extrair o refreshToken o token de contexto
        Dim refreshToken As String = contextToken.RefreshToken

        If [String].IsNullOrEmpty(refreshToken) Then
            Return Nothing
        End If

        Dim targetRealm As String = If(Realm, contextToken.Realm)

        Return GetAccessToken(refreshToken, targetPrincipalName, targetHost, targetRealm)
    End Function

    ''' <summary>
    ''' Usa o código de autorização especificado para recuperar um token de acesso do ACS para chamar a entidade de segurança especificada
    ''' no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
    ''' Nada, a configuração "Realm" em web.config será usada em seu lugar.
    ''' </summary>
    ''' <param name="authorizationCode">Código de autorização a trocar pelo token de acesso</param>
    ''' <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
    ''' <param name="targetHost">Autoridade de URL da entidade de destino</param>
    ''' <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
    ''' <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
    ''' <returns>Um token de acesso com uma audiência da entidade de destino</returns>
    Public Shared Function GetAccessToken(authorizationCode As String, targetPrincipalName As String, targetHost As String, targetRealm As String, redirectUri As Uri) As OAuthTokenResponse

        If targetRealm Is Nothing Then
            targetRealm = Realm
        End If

        Dim resource As String = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm)
        Dim formattedClientId As String = GetFormattedPrincipal(ClientId, Nothing, targetRealm)
        Dim acsUri As String = AcsMetadataParser.GetStsUrl(targetRealm)
        Dim oauthResponse As OAuthTokenResponse = Nothing

        Try
            oauthResponse = OAuthClient.GetAccessTokenWithAuthorizationCode(acsUri, formattedClientId, ClientSecret, authorizationCode, redirectUri.AbsoluteUri, resource)

        Catch ex As WebException
            If Not [String].IsNullOrEmpty(SecondaryClientSecret) Then
                oauthResponse = OAuthClient.GetAccessTokenWithAuthorizationCode(acsUri, formattedClientId, SecondaryClientSecret, authorizationCode, redirectUri.AbsoluteUri, resource)
            Else
                Using sr As New StreamReader(ex.Response.GetResponseStream())
                    Dim responseText As String = sr.ReadToEnd()
                    Throw New WebException(ex.Message + " - " + responseText, ex)
                End Using
            End If
        End Try

        Return oauthResponse
    End Function

    ''' <summary>
    ''' Usa o token de atualização especificado para recuperar um token de acesso do ACS para chamar a entidade de segurança especificada
    ''' no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
    ''' Nada, a configuração "Realm" em web.config será usada em seu lugar.
    ''' </summary>
    ''' <param name="refreshToken">Atualizar token a trocar pelo token de acesso</param>
    ''' <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
    ''' <param name="targetHost">Autoridade de URL da entidade de destino</param>
    ''' <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
    ''' <returns>Um token de acesso com uma audiência da entidade de destino</returns>
    Public Shared Function GetAccessToken(refreshToken As String, targetPrincipalName As String, targetHost As String, targetRealm As String) As OAuthTokenResponse

        If targetRealm Is Nothing Then
            targetRealm = Realm
        End If

        Dim resource As String = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm)
        Dim formattedClientId As String = GetFormattedPrincipal(ClientId, Nothing, targetRealm)
        Dim acsUri As String = AcsMetadataParser.GetStsUrl(targetRealm)
        Dim oauthResponse As OAuthTokenResponse = Nothing

        Try
            oauthResponse = OAuthClient.GetAccessTokenWithRefreshToken(acsUri, formattedClientId, ClientSecret, refreshToken, resource)

        Catch ex As WebException
            If Not [String].IsNullOrEmpty(SecondaryClientSecret) Then
                oauthResponse = OAuthClient.GetAccessTokenWithRefreshToken(acsUri, formattedClientId, SecondaryClientSecret, refreshToken, resource)
            Else
                Using sr As New StreamReader(ex.Response.GetResponseStream())
                    Dim responseText As String = sr.ReadToEnd()
                    Throw New WebException(ex.Message + " - " + responseText, ex)
                End Using
            End If
        End Try

        Return oauthResponse
    End Function

    ''' <summary>
    ''' Recupera um token de acesso somente de aplicativo do ACS para chamar a entidade especificada
    ''' no targetHost especificado. O targetHost precisa estar registrado para a entidade de segurança de destino. Se o realm especificado for
    ''' Nada, a configuração "Realm" em web.config será usada em seu lugar.
    ''' </summary>
    ''' <param name="targetPrincipalName">Nome da entidade de destino da qual recuperar um token de acesso</param>
    ''' <param name="targetHost">Autoridade de URL da entidade de destino</param>
    ''' <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
    ''' <returns>Um token de acesso com uma audiência da entidade de destino</returns>
    Public Shared Function GetAppOnlyAccessToken(targetPrincipalName As String, targetHost As String, targetRealm As String) As OAuthTokenResponse

        If targetRealm Is Nothing Then
            targetRealm = Realm
        End If

        Dim resource As String = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm)
        Dim formattedClientId As String = GetFormattedPrincipal(ClientId, HostedAppHostName, targetRealm)
        Dim acsUri As String = AcsMetadataParser.GetStsUrl(targetRealm)
        Dim oauthResponse As OAuthTokenResponse = Nothing

        Try
            oauthResponse = OAuthClient.GetAccessTokenWithClientCredentials(acsUri, formattedClientId, ClientSecret, resource)

        Catch ex As WebException
            If Not [String].IsNullOrEmpty(SecondaryClientSecret) Then
                oauthResponse = OAuthClient.GetAccessTokenWithClientCredentials(acsUri, formattedClientId, SecondaryClientSecret, resource)
            Else
                Using sr As New StreamReader(ex.Response.GetResponseStream())
                    Dim responseText As String = sr.ReadToEnd()
                    Throw New WebException(ex.Message + " - " + responseText, ex)
                End Using
            End If
        End Try

        Return oauthResponse
    End Function

    ''' <summary>
    ''' Cria um contexto de cliente com base nas propriedades de um receptor de eventos remoto
    ''' </summary>
    ''' <param name="properties">Propriedades de um receptor de eventos remoto</param>
    ''' <returns>Um ClientContext pronto para chamar a Web em que o evento foi originado</returns>
    Public Shared Function CreateRemoteEventReceiverClientContext(properties As SPRemoteEventProperties) As ClientContext
        Dim sharepointUrl As Uri
        If properties.ListEventProperties IsNot Nothing Then
            sharepointUrl = New Uri(properties.ListEventProperties.WebUrl)
        ElseIf properties.ItemEventProperties IsNot Nothing Then
            sharepointUrl = New Uri(properties.ItemEventProperties.WebUrl)
        ElseIf properties.WebEventProperties IsNot Nothing Then
            sharepointUrl = New Uri(properties.WebEventProperties.FullUrl)
        Else
            Return Nothing
        End If

        If IsHighTrustApp() Then
            Return GetS2SClientContextWithWindowsIdentity(sharepointUrl, Nothing)
        End If

        Return CreateAcsClientContextForUrl(properties, sharepointUrl)

    End Function

    ''' <summary>
    ''' Cria um contexto de cliente com base nas propriedades de um evento de suplemento
    ''' </summary>
    ''' <param name="properties">Propriedades de um de evento e suplemento</param>
    ''' <param name="useAppWeb">True para usar o aplicativo Web como destino, false para usar o host Web como destino</param>
    ''' <returns>Um ClientContext pronto para chamar o aplicativo Web ou o site pai</returns>
    Public Shared Function CreateAppEventClientContext(properties As SPRemoteEventProperties, useAppWeb As Boolean) As ClientContext
        If properties.AppEventProperties Is Nothing Then
            Return Nothing
        End If

        Dim sharepointUrl As Uri = If(useAppWeb, properties.AppEventProperties.AppWebFullUrl, properties.AppEventProperties.HostWebFullUrl)
        If IsHighTrustApp() Then
            Return GetS2SClientContextWithWindowsIdentity(sharepointUrl, Nothing)
        End If

        Return CreateAcsClientContextForUrl(properties, sharepointUrl)
    End Function

    ''' <summary>
    ''' Recupera um token de acesso do ACS usando o código de autorização especificado e usa esse token de acesso para
    ''' criar um contexto de cliente
    ''' </summary>
    ''' <param name="targetUrl">URL do site do SharePoint de destino</param>
    ''' <param name="authorizationCode">Código de autorização a usar ao recuperar o token de acesso do ACS</param>
    ''' <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
    ''' <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
    Public Shared Function GetClientContextWithAuthorizationCode(targetUrl As String, authorizationCode As String, redirectUri As Uri) As ClientContext
        Return GetClientContextWithAuthorizationCode(targetUrl, SharePointPrincipal, authorizationCode, GetRealmFromTargetUrl(New Uri(targetUrl)), redirectUri)
    End Function

    ''' <summary>
    ''' Recupera um token de acesso do ACS usando o código de autorização especificado e usa esse token de acesso para
    ''' criar um contexto de cliente
    ''' </summary>
    ''' <param name="targetUrl">URL do site do SharePoint de destino</param>
    ''' <param name="targetPrincipalName">Nome da entidade do SharePoint de destino</param>
    ''' <param name="authorizationCode">Código de autorização a usar ao recuperar o token de acesso do ACS</param>
    ''' <param name="targetRealm">Realm a usar para a nameid e audiência do token de acesso</param>
    ''' <param name="redirectUri">URI de redirecionamento registrado para este suplemento</param>
    ''' <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
    Public Shared Function GetClientContextWithAuthorizationCode(targetUrl As String, targetPrincipalName As String, authorizationCode As String, targetRealm As String, redirectUri As Uri) As ClientContext
        Dim targetUri As New Uri(targetUrl)

        Dim accessToken As String = GetAccessToken(authorizationCode, targetPrincipalName, targetUri.Authority, targetRealm, redirectUri).AccessToken

        Return GetClientContextWithAccessToken(targetUrl, accessToken)
    End Function

    ''' <summary>
    ''' Usa o token de acesso especificado criar um contexto de cliente
    ''' </summary>
    ''' <param name="targetUrl">URL do site do SharePoint de destino</param>
    ''' <param name="accessToken">O token de acesso a ser usado ao chamar a targetUrl especificada</param>
    ''' <returns>Um ClientContext pronto para chamar a targetUrl com o token de acesso especificado</returns>
    Public Shared Function GetClientContextWithAccessToken(targetUrl As String, accessToken As String) As ClientContext
        Dim clientContext As New ClientContext(targetUrl)

        clientContext.AuthenticationMode = ClientAuthenticationMode.Anonymous
        clientContext.FormDigestHandlingEnabled = False

        AddHandler clientContext.ExecutingWebRequest, Sub(oSender As Object, webRequestEventArgs As WebRequestEventArgs)
                                                          webRequestEventArgs.WebRequestExecutor.RequestHeaders("Authorization") = "Bearer " & accessToken
                                                      End Sub
        Return clientContext
    End Function

    ''' <summary>
    ''' Recupera um token de acesso do ACS usando o token de contexto especificado e usa esse token de acesso para criar
    ''' um contexto de cliente
    ''' </summary>
    ''' <param name="targetUrl">URL do site do SharePoint de destino</param>
    ''' <param name="contextTokenString">Token de contexto recebido do site do SharePoint de destino</param>
    ''' <param name="appHostUrl">Autoridade da URL do suplemento hospedado. Se isso for Nada, o valor em HostedAppHostName
    ''' de web.config será usado em seu lugar</param>
    ''' <returns>Um ClientContext pronto para chamar a targetUrl com um token de acesso válido</returns>
    Public Shared Function GetClientContextWithContextToken(targetUrl As String, contextTokenString As String, appHostUrl As String) As ClientContext
        Dim contextToken As SharePointContextToken = ReadAndValidateContextToken(contextTokenString, appHostUrl)

        Dim targetUri As New Uri(targetUrl)

        Dim accessToken As String = GetAccessToken(contextToken, targetUri.Authority).AccessToken

        Return GetClientContextWithAccessToken(targetUrl, accessToken)
    End Function

    ''' <summary>
    ''' Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar consentimento e retornar
    ''' um código de autorização.
    ''' </summary>
    ''' <param name="contextUrl">URL absoluta do site do SharePoint</param>
    ''' <param name="scope">Permissões delimitadas por espaço a solicitar do site do SharePoint em formato "abreviado"
    ''' (por exemplo, "Web.Read Site.Write")</param>
    ''' <returns>URL da página de autorização OAuth do site do SharePoint</returns>
    Public Shared Function GetAuthorizationUrl(contextUrl As String, scope As String) As String
        Return String.Format("{0}{1}?IsDlg=1&client_id={2}&scope={3}&response_type=code", EnsureTrailingSlash(contextUrl), AuthorizationPage, ClientId, scope)
    End Function

    ''' <summary>
    ''' Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar consentimento e retornar
    ''' um código de autorização.
    ''' </summary>
    ''' <param name="contextUrl">URL absoluta do site do SharePoint</param>
    ''' <param name="scope">Permissões delimitadas por espaço a solicitar do site do SharePoint em formato "abreviado"
    ''' (por exemplo, "Web.Read Site.Write")</param>
    ''' <param name="redirectUri">O URI para o qual o SharePoint deve redirecionar o navegador depois que o consentimento é
    ''' concedido</param>
    ''' <returns>URL da página de autorização OAuth do site do SharePoint</returns>
    Public Shared Function GetAuthorizationUrl(contextUrl As String, scope As String, redirectUri As String) As String
        Return String.Format("{0}{1}?IsDlg=1&client_id={2}&scope={3}&response_type=code&redirect_uri={4}", EnsureTrailingSlash(contextUrl), AuthorizationPage, ClientId, scope, redirectUri)
    End Function

    ''' <summary>
    ''' Retorna a URL do SharePoint para a qual o suplemento deve redirecionar o navegador para solicitar um novo token de contexto.
    ''' </summary>
    ''' <param name="contextUrl">URL absoluta do site do SharePoint</param>
    ''' <param name="redirectUri">URI para o qual o SharePoint deve redirecionar o navegador com um token de contexto</param>
    ''' <returns>URL da página de redirecionamento do token de contexto do site do SharePoint</returns>
    Public Shared Function GetAppContextTokenRequestUrl(contextUrl As String, redirectUri As String) As String
        Return String.Format("{0}{1}?client_id={2}&redirect_uri={3}", EnsureTrailingSlash(contextUrl), RedirectPage, ClientId, redirectUri)
    End Function

    ''' <summary>
    ''' Recupera um token de acesso S2S assinado pelo certificado privado do aplicativo em nome do especificado
    ''' WindowsIdentity e destinado ao SharePoint no targetApplicationUri. Se nenhum Realm estiver especificado em
    ''' web.config, um desafio de autenticação será emitido ao targetApplicationUri para que este o descubra.
    ''' </summary>
    ''' <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
    ''' <param name="identity">Identidade do Windows do usuário em cujo nome criar o token de acesso</param>
    ''' <returns>Um token de acesso com uma audiência da entidade de destino</returns>
    Public Shared Function GetS2SAccessTokenWithWindowsIdentity(targetApplicationUri As Uri, identity As WindowsIdentity) As String
        Dim targetRealm As String = If(String.IsNullOrEmpty(Realm), GetRealmFromTargetUrl(targetApplicationUri), Realm)

        Dim claims As Claim() = If(identity IsNot Nothing, GetClaimsWithWindowsIdentity(identity), Nothing)

        Return GetS2SAccessTokenWithClaims(targetApplicationUri.Authority, targetRealm, claims)
    End Function

    ''' <summary>
    ''' Recupera um contexto do cliente S2S com um token de acesso assinado pelo certificado privado do aplicativo em
    ''' em nome do WindowsIdentity especificado e pretendido para o aplicativo no targetApplicationUri usando o
    ''' targetRealm. Se nenhum Realm for especificado em web.config, um desafio de autenticação será emitido ao
    ''' targetApplicationUri para que este o descubra.
    ''' </summary>
    ''' <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
    ''' <param name="identity">Identidade do Windows do usuário em cujo nome criar o token de acesso</param>
    ''' <returns>Um ClientContext usando um token de acesso com uma audiência do aplicativo de destino</returns>
    Public Shared Function GetS2SClientContextWithWindowsIdentity(targetApplicationUri As Uri, identity As WindowsIdentity) As ClientContext
        Dim targetRealm As String = If(String.IsNullOrEmpty(Realm), GetRealmFromTargetUrl(targetApplicationUri), Realm)

        Dim claims As Claim() = If(identity IsNot Nothing, GetClaimsWithWindowsIdentity(identity), Nothing)

        Dim accessToken As String = GetS2SAccessTokenWithClaims(targetApplicationUri.Authority, targetRealm, claims)

        Return GetClientContextWithAccessToken(targetApplicationUri.ToString(), accessToken)
    End Function

    ''' <summary>
    ''' Obter o realm de autenticação do SharePoint
    ''' </summary>
    ''' <param name="targetApplicationUri">URL do site do SharePoint de destino</param>
    ''' <returns>Representação do GUID do realm em cadeia de caracteres</returns>
    Public Shared Function GetRealmFromTargetUrl(targetApplicationUri As Uri) As String
        Dim request As WebRequest = HttpWebRequest.Create(targetApplicationUri.ToString() & "/_vti_bin/client.svc")
        request.Headers.Add("Authorization: Bearer ")

        Try
            request.GetResponse().Close()
        Catch e As WebException
            If e.Response Is Nothing Then
                Return Nothing
            End If

            Dim bearerResponseHeader As String = e.Response.Headers("WWW-Authenticate")
            If String.IsNullOrEmpty(bearerResponseHeader) Then
                Return Nothing
            End If

            Const bearer As String = "Bearer realm="""
            Dim bearerIndex As Integer = bearerResponseHeader.IndexOf(bearer, StringComparison.Ordinal)
            If bearerIndex < 0 Then
                Return Nothing
            End If

            Dim realmIndex As Integer = bearerIndex + bearer.Length

            If bearerResponseHeader.Length >= realmIndex + 36 Then
                Dim targetRealm As String = bearerResponseHeader.Substring(realmIndex, 36)

                Dim realmGuid As Guid

                If Guid.TryParse(targetRealm, realmGuid) Then
                    Return targetRealm
                End If
            End If
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Determina se este é um suplemento de alta confiança.
    ''' </summary>
    ''' <returns>True se este for um suplemento de alta confiança.</returns>
    Public Shared Function IsHighTrustApp() As Boolean
        Return SigningCredentials IsNot Nothing
    End Function

    ''' <summary>
    ''' Garante que a URL especificada termina com '/' se ela não for nula ou vazia.
    ''' </summary>
    ''' <param name="url">A URL.</param>
    ''' <returns>A URL terminando com '/', se ela não for nula ou vazia.</returns>
    Public Shared Function EnsureTrailingSlash(url As String) As String
        If Not String.IsNullOrEmpty(url) AndAlso url(url.Length - 1) <> "/"c Then
            Return url + "/"
        End If

        Return url
    End Function

    ''' <summary>
    ''' Retorna o horário da Época atual em segundos
    ''' </summary>
    ''' <returns>Horário da Época em segundos</returns>
    Public Shared Function EpochTimeNow() As Long
        Return (DateTime.UtcNow - New DateTime(1970, 1, 1).ToUniversalTime()).TotalSeconds
    End Function

#End Region

#Region "campos particulares"

    '
    ' Constantes de Configuração
    '

    Private Const AuthorizationPage As String = "_layouts/15/OAuthAuthorize.aspx"
    Private Const RedirectPage As String = "_layouts/15/AppRedirect.aspx"
    Private Const AcsPrincipalName As String = "00000001-0000-0000-c000-000000000000"
    Private Const AcsMetadataEndPointRelativeUrl As String = "metadata/json/1"
    Private Const S2SProtocol As String = "OAuth2"
    Private Const DelegationIssuance As String = "DelegationIssuance1.0"
    Private Const NameIdentifierClaimType As String = "nameid"
    Private Const TrustedForImpersonationClaimType As String = "trustedfordelegation"
    Private Const ActorTokenClaimType As String = "actortoken"

    '
    ' Constantes de Ambiente
    '

    Private Shared GlobalEndPointPrefix As String = "accounts"
    Private Shared AcsHostUrl As String = "accesscontrol.windows.net"

    '
    ' Configuração de suplemento hospedado
    '
    Private Shared ReadOnly ClientId As String = If(String.IsNullOrEmpty(WebConfigurationManager.AppSettings.[Get]("ClientId")), WebConfigurationManager.AppSettings.[Get]("HostedAppName"), WebConfigurationManager.AppSettings.[Get]("ClientId"))

    Private Shared ReadOnly IssuerId As String = If(String.IsNullOrEmpty(WebConfigurationManager.AppSettings.[Get]("IssuerId")), ClientId, WebConfigurationManager.AppSettings.[Get]("IssuerId"))

    Private Shared ReadOnly HostedAppHostName As String = WebConfigurationManager.AppSettings.[Get]("HostedAppHostName")

    Private Shared ReadOnly HostedAppHostNameOverride As String = WebConfigurationManager.AppSettings.[Get]("HostedAppHostNameOverride")

    Private Shared ReadOnly ClientSecret As String = If(String.IsNullOrEmpty(WebConfigurationManager.AppSettings.[Get]("ClientSecret")), WebConfigurationManager.AppSettings.[Get]("HostedAppSigningKey"), WebConfigurationManager.AppSettings.[Get]("ClientSecret"))

    Private Shared ReadOnly SecondaryClientSecret As String = WebConfigurationManager.AppSettings.[Get]("SecondaryClientSecret")

    Private Shared ReadOnly Realm As String = WebConfigurationManager.AppSettings.[Get]("Realm")

    Private Shared ReadOnly ServiceNamespace As String = WebConfigurationManager.AppSettings.[Get]("Realm")

    Private Shared ReadOnly ClientSigningCertificatePath As String = WebConfigurationManager.AppSettings.[Get]("ClientSigningCertificatePath")

    Private Shared ReadOnly ClientSigningCertificatePassword As String = WebConfigurationManager.AppSettings.[Get]("ClientSigningCertificatePassword")

    Private Shared ReadOnly ClientCertificate As X509Certificate2 = If((String.IsNullOrEmpty(ClientSigningCertificatePath) OrElse String.IsNullOrEmpty(ClientSigningCertificatePassword)), Nothing, New X509Certificate2(ClientSigningCertificatePath, ClientSigningCertificatePassword))

    Private Shared ReadOnly SigningCredentials As X509SigningCredentials = If(ClientCertificate Is Nothing, Nothing, New X509SigningCredentials(ClientCertificate, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.RsaSha256))

#End Region

#Region "métodos privados"

    Private Shared Function CreateAcsClientContextForUrl(properties As SPRemoteEventProperties, sharepointUrl As Uri) As ClientContext
        Dim contextTokenString As String = properties.ContextToken

        If [String].IsNullOrEmpty(contextTokenString) Then
            Return Nothing
        End If

        Dim contextToken As SharePointContextToken = ReadAndValidateContextToken(contextTokenString, OperationContext.Current.IncomingMessageHeaders.To.Host)

        Dim accessToken As String = GetAccessToken(contextToken, sharepointUrl.Authority).AccessToken
        Return GetClientContextWithAccessToken(sharepointUrl.ToString(), accessToken)
    End Function

    Private Shared Function GetAcsMetadataEndpointUrl() As String
        Return Path.Combine(GetAcsGlobalEndpointUrl(), AcsMetadataEndPointRelativeUrl)
    End Function

    Private Shared Function GetFormattedPrincipal(principalName As String, hostName As String, targetRealm As String) As String
        If Not [String].IsNullOrEmpty(hostName) Then
            Return [String].Format(CultureInfo.InvariantCulture, "{0}/{1}@{2}", principalName, hostName, targetRealm)
        End If

        Return [String].Format(CultureInfo.InvariantCulture, "{0}@{1}", principalName, targetRealm)
    End Function

    Private Shared Function GetAcsPrincipalName(targetRealm As String) As String
        Return GetFormattedPrincipal(AcsPrincipalName, New Uri(GetAcsGlobalEndpointUrl()).Host, targetRealm)
    End Function

    Private Shared Function GetAcsGlobalEndpointUrl() As String
        Return [String].Format(CultureInfo.InvariantCulture, "https://{0}.{1}/", GlobalEndPointPrefix, AcsHostUrl)
    End Function

    Private Shared Function CreateJwtSecurityTokenHandler() As JwtSecurityTokenHandler
        Return New JwtSecurityTokenHandler()
    End Function

    Private Shared Function GetS2SAccessTokenWithClaims(targetApplicationHostName As String, targetRealm As String, claims As IEnumerable(Of Claim)) As String
        Return IssueToken(ClientId, IssuerId, targetRealm, SharePointPrincipal, targetRealm, targetApplicationHostName, True,
                          claims, claims Is Nothing)
    End Function

    Private Shared Function GetClaimsWithWindowsIdentity(identity As WindowsIdentity) As Claim()
        Dim claims As Claim() = New Claim() _
                {New Claim(NameIdentifierClaimType, identity.User.Value.ToLower()),
                 New Claim("nii", "urn:office:idp:activedirectory")}
        Return claims
    End Function

    Private Shared Function IssueToken(sourceApplication As String, issuerApplication As String, sourceRealm As String, targetApplication As String, targetRealm As String, targetApplicationHostName As String, trustedForDelegation As Boolean,
                                       claims As IEnumerable(Of Claim), Optional appOnly As Boolean = False) As String
        If SigningCredentials Is Nothing Then
            Throw New InvalidOperationException("SigningCredentials was not initialized")
        End If

        '#Region "Token de ator"

        Dim issuer As String = If(String.IsNullOrEmpty(sourceRealm), issuerApplication, String.Format("{0}@{1}", issuerApplication, sourceRealm))
        Dim nameid As String = If(String.IsNullOrEmpty(sourceRealm), sourceApplication, String.Format("{0}@{1}", sourceApplication, sourceRealm))
        Dim audience As String = String.Format("{0}/{1}@{2}", targetApplication, targetApplicationHostName, targetRealm)

        Dim actorClaims As New List(Of Claim)()
        actorClaims.Add(New Claim(NameIdentifierClaimType, nameid))
        If trustedForDelegation AndAlso Not appOnly Then
            actorClaims.Add(New Claim(TrustedForImpersonationClaimType, "true"))
        End If

        ' Criar Token
        Dim actorToken As New JwtSecurityToken(issuer:=issuer, audience:=audience, claims:=actorClaims, notBefore:=DateTime.UtcNow, expires:=DateTime.UtcNow.Add(HighTrustAccessTokenLifetime), signingCredentials:=SigningCredentials)

        Dim actorTokenString As String = New JwtSecurityTokenHandler().WriteToken(actorToken)

        If appOnly Then
            ' O token somente de aplicativo é o mesmo que o token de ator para o caso delegado
            Return actorTokenString
        End If

        '#End Region

        '#Region "Token externo"

        Dim outerClaims As List(Of Claim) = If(claims Is Nothing, New List(Of Claim)(), New List(Of Claim)(claims))
        outerClaims.Add(New Claim(ActorTokenClaimType, actorTokenString))

        ' o emissor do token externo deve corresponder à nameid do token de ator
        Dim jsonToken As New JwtSecurityToken(nameid, audience, outerClaims, DateTime.UtcNow, DateTime.UtcNow.Add(HighTrustAccessTokenLifetime))

        Dim accessToken As String = New JwtSecurityTokenHandler().WriteToken(jsonToken)

        '#End Region

        Return accessToken
    End Function

#End Region

#Region "AcsMetadataParser"

    ' Essa classe é usada para obter o documento MetaData do ponto de extremidade do STS global. Ela contém
    ' métodos para analisar o documento MetaData e obter pontos de extremidade e certificado STS.
    Public NotInheritable Class AcsMetadataParser
        Private Sub New()
        End Sub

        Public Shared Function GetAcsSigningCert(realm As String) As X509Certificate2
            Dim document As JsonMetadataDocument = GetMetadataDocument(realm)

            If document.keys IsNot Nothing AndAlso document.keys.Count > 0 Then
                Dim signingKey As JsonKey = document.keys(0)

                If signingKey IsNot Nothing AndAlso signingKey.keyValue IsNot Nothing Then
                    Return New X509Certificate2(Encoding.UTF8.GetBytes(signingKey.keyValue.value))
                End If
            End If

            Throw New Exception("Metadata document does not contain ACS signing certificate.")
        End Function

        Public Shared Function GetDelegationServiceUrl(realm As String) As String
            Dim document As JsonMetadataDocument = GetMetadataDocument(realm)

            Dim delegationEndpoint As JsonEndpoint = document.endpoints.SingleOrDefault(Function(e) e.protocol = DelegationIssuance)

            If delegationEndpoint IsNot Nothing Then
                Return delegationEndpoint.location
            End If

            Throw New Exception("Metadata document does not contain Delegation Service endpoint Url")
        End Function

        Private Shared Function GetMetadataDocument(realm As String) As JsonMetadataDocument
            Dim acsMetadataEndpointUrlWithRealm As String = [String].Format(CultureInfo.InvariantCulture, "{0}?realm={1}", GetAcsMetadataEndpointUrl(), realm)
            Dim acsMetadata As Byte()
            Using webClient As New WebClient()
                acsMetadata = webClient.DownloadData(acsMetadataEndpointUrlWithRealm)
            End Using
            Dim jsonResponseString As String = Encoding.UTF8.GetString(acsMetadata)

            Dim serializer As New JavaScriptSerializer()
            Dim document As JsonMetadataDocument = serializer.Deserialize(Of JsonMetadataDocument)(jsonResponseString)

            If document Is Nothing Then
                Throw New Exception("No metadata document found at the global endpoint " & acsMetadataEndpointUrlWithRealm)
            End If

            Return document
        End Function

        Public Shared Function GetStsUrl(realm As String) As String
            Dim document As JsonMetadataDocument = GetMetadataDocument(realm)

            Dim s2sEndpoint As JsonEndpoint = document.endpoints.SingleOrDefault(Function(e) e.protocol = S2SProtocol)

            If s2sEndpoint IsNot Nothing Then
                Return s2sEndpoint.location
            End If

            Throw New Exception("Metadata document does not contain STS endpoint url")
        End Function

        Private Class JsonMetadataDocument
            Public Property serviceName() As String
                Get
                    Return m_serviceName
                End Get
                Set(value As String)
                    m_serviceName = value
                End Set
            End Property

            Private m_serviceName As String

            Public Property endpoints() As List(Of JsonEndpoint)
                Get
                    Return m_endpoints
                End Get
                Set(value As List(Of JsonEndpoint))
                    m_endpoints = value
                End Set
            End Property

            Private m_endpoints As List(Of JsonEndpoint)

            Public Property keys() As List(Of JsonKey)
                Get
                    Return m_keys
                End Get
                Set(value As List(Of JsonKey))
                    m_keys = value
                End Set
            End Property

            Private m_keys As List(Of JsonKey)
        End Class

        Private Class JsonEndpoint
            Public Property location() As String
                Get
                    Return m_location
                End Get
                Set(value As String)
                    m_location = value
                End Set
            End Property

            Private m_location As String

            Public Property protocol() As String
                Get
                    Return m_protocol
                End Get
                Set(value As String)
                    m_protocol = value
                End Set
            End Property

            Private m_protocol As String

            Public Property usage() As String
                Get
                    Return m_usage
                End Get
                Set(value As String)
                    m_usage = value
                End Set
            End Property

            Private m_usage As String
        End Class

        Private Class JsonKeyValue
            Public Property type() As String
                Get
                    Return m_type
                End Get
                Set(value As String)
                    m_type = value
                End Set
            End Property

            Private m_type As String

            Public Property value() As String
                Get
                    Return m_value
                End Get
                Set(value As String)
                    m_value = value
                End Set
            End Property

            Private m_value As String
        End Class

        Private Class JsonKey
            Public Property usage() As String
                Get
                    Return m_usage
                End Get
                Set(value As String)
                    m_usage = value
                End Set
            End Property

            Private m_usage As String

            Public Property keyValue() As JsonKeyValue
                Get
                    Return m_keyValue
                End Get
                Set(value As JsonKeyValue)
                    m_keyValue = value
                End Set
            End Property

            Private m_keyValue As JsonKeyValue
        End Class
    End Class

#End Region
End Class

''' <summary>
''' Um JwtSecurityToken gerado pelo SharePoint para autenticar em um aplicativo de terceiros e permitir retornos de chamada usando um token de atualização
''' </summary>
Public Class SharePointContextToken
    Inherits JwtSecurityToken

    Public Shared Function Create(contextToken As JwtSecurityToken) As SharePointContextToken
        Return New SharePointContextToken(contextToken.Issuer, contextToken.Audiences.FirstOrDefault, contextToken.ValidFrom, contextToken.ValidTo, contextToken.Claims)
    End Function

    Public Sub New(issuer As String, audience As String, validFrom As DateTime, validTo As DateTime, claims As IEnumerable(Of Claim))
        MyBase.New(issuer, audience, claims, validFrom, validTo)
    End Sub

    Public Sub New(issuer As String, audience As String, validFrom As DateTime, validTo As DateTime, claims As IEnumerable(Of Claim), issuerToken As SecurityToken, actorToken As JwtSecurityToken)
        ' Este método É fornecido para manter a compatibilidade com as versões anteriores do TokenHelper.
        ' A versão atual do JwtSecurityToken Não tem um construtor que use todos os parâmetros acima.

        MyBase.New(issuer, audience, claims, validFrom, validTo, actorToken.SigningCredentials)
    End Sub

    Public Sub New(issuer As String, audience As String, validFrom As DateTime, validTo As DateTime, claims As IEnumerable(Of Claim), signingCredentials As SigningCredentials)
        MyBase.New(issuer, audience, claims, validFrom, validTo, signingCredentials)
    End Sub

    Public ReadOnly Property NameId() As String
        Get
            Return GetClaimValue(Me, "nameid")
        End Get
    End Property

    ''' <summary>
    ''' A parte do nome da entidade da declaração de "appctxsender" do token de contexto
    ''' </summary>
    Public ReadOnly Property TargetPrincipalName() As String
        Get
            Dim appctxsender As String = GetClaimValue(Me, "appctxsender")

            If appctxsender Is Nothing Then
                Return Nothing
            End If

            Return appctxsender.Split("@"c)(0)
        End Get
    End Property

    ''' <summary>
    ''' A declaração "refreshtoken" do token de contexto
    ''' </summary>
    Public ReadOnly Property RefreshToken() As String
        Get
            Return GetClaimValue(Me, "refreshtoken")
        End Get
    End Property

    ''' <summary>
    ''' A declaração "CacheKey" do token de contexto
    ''' </summary>
    Public ReadOnly Property CacheKey() As String
        Get
            Dim appctx As String = GetClaimValue(Me, "appctx")
            If appctx Is Nothing Then
                Return Nothing
            End If

            Dim ctx As New ClientContext("http://tempuri.org")
            Dim dict As Dictionary(Of String, Object) = DirectCast(ctx.ParseObjectFromJsonString(appctx), Dictionary(Of String, Object))
            Dim cacheKeyString As String = DirectCast(dict("CacheKey"), String)

            Return cacheKeyString
        End Get
    End Property

    ''' <summary>
    ''' A declaração "SecurityTokenServiceUri" do token de contexto
    ''' </summary>
    Public ReadOnly Property SecurityTokenServiceUri() As String
        Get
            Dim appctx As String = GetClaimValue(Me, "appctx")
            If appctx Is Nothing Then
                Return Nothing
            End If

            Dim ctx As New ClientContext("http://tempuri.org")
            Dim dict As Dictionary(Of String, Object) = DirectCast(ctx.ParseObjectFromJsonString(appctx), Dictionary(Of String, Object))
            Dim securityTokenServiceUriString As String = DirectCast(dict("SecurityTokenServiceUri"), String)

            Return securityTokenServiceUriString
        End Get
    End Property

    ''' <summary>
    ''' A parte da realm da declaração de "audience" do token de contexto
    ''' </summary>
    Public ReadOnly Property Realm() As String
        Get
            For Each aud As String In Audiences
                Dim tokenRealm As String = aud.Substring(aud.IndexOf("@"c) + 1)
                If String.IsNullOrEmpty(tokenRealm) Then
                    Continue For
                Else
                    Return tokenRealm
                End If
            Next
            Return Nothing
        End Get
    End Property

    Private Shared Function GetClaimValue(token As JwtSecurityToken, claimType As String) As String
        If token Is Nothing Then
            Throw New ArgumentNullException("token")
        End If

        For Each claim As Claim In token.Claims
            If StringComparer.Ordinal.Equals(claim.Type, claimType) Then
                Return claim.Value
            End If
        Next

        Return Nothing
    End Function
End Class

''' <summary>
''' Representa um token de segurança que contém várias chaves de segurança geradas usando algoritmos simétricos.
''' </summary>
Public Class MultipleSymmetricKeySecurityToken
    Inherits SecurityToken

    ''' <summary>
    ''' Inicializa uma nova instância da classe MultipleSymmetricKeySecurityToken.
    ''' </summary>
    ''' <param name="keys">Uma enumeração das matrizes de bytes que contêm as chaves simétricas.</param>
    Public Sub New(keys As IEnumerable(Of Byte()))
        Me.New(Microsoft.IdentityModel.Tokens.UniqueId.CreateUniqueId(), keys)
    End Sub

    ''' <summary>
    ''' Inicializa uma nova instância da classe MultipleSymmetricKeySecurityToken.
    ''' </summary>
    ''' <param name="tokenId">O identificador exclusivo do token de segurança.</param>
    ''' <param name="keys">Uma enumeração das matrizes de bytes que contêm as chaves simétricas.</param>
    Public Sub New(tokenId As String, keys As IEnumerable(Of Byte()))
        If keys Is Nothing Then
            Throw New ArgumentNullException("keys")
        End If

        If String.IsNullOrEmpty(tokenId) Then
            Throw New ArgumentException("Value cannot be a null or empty string.", "tokenId")
        End If

        For Each key As Byte() In keys
            If key.Length <= 0 Then
                Throw New ArgumentException("The key length must be greater then zero.", "keys")
            End If
        Next

        m_id = tokenId
        m_effectiveTime = DateTime.UtcNow
        m_securityKeys = CreateSymmetricSecurityKeys(keys)
    End Sub

    ''' <summary>
    ''' Obtém um identificador exclusivo do token de segurança.
    ''' </summary>
    Public Overrides ReadOnly Property Id As String
        Get
            Return m_id
        End Get
    End Property

    ''' <summary>
    ''' Obtém as chaves de criptografia associadas com o token de segurança.
    ''' </summary>
    Public Overrides ReadOnly Property SecurityKeys() As ReadOnlyCollection(Of SecurityKey)
        Get
            Return m_securityKeys.AsReadOnly()
        End Get
    End Property

    ''' <summary>
    ''' Obtém o primeiro instante no tempo em que esse token de segurança é válido.
    ''' </summary>
    Public Overrides ReadOnly Property ValidFrom As DateTime
        Get
            Return m_effectiveTime
        End Get
    End Property

    ''' <summary>
    ''' Obtém o último instante no tempo em que esse token de segurança é válido.
    ''' </summary>
    Public Overrides ReadOnly Property ValidTo As DateTime
        Get
            ' Nunca expirar
            Return Date.MaxValue
        End Get
    End Property

    ''' <summary>
    ''' Retorna um valor que indica se o identificador de chave para esta instância pode ser resolvido para o identificador de chave especificado.
    ''' </summary>
    ''' <param name="keyIdentifierClause">Uma SecurityKeyIdentifierClause para comparar à instância.</param>
    ''' <returns>true se keyIdentifierClause for uma SecurityKeyIdentifierClause e se tiver o mesmo identificador exclusivo que a propriedade Id; caso contrário, false.</returns>
    Public Overrides Function MatchesKeyIdentifierClause(keyIdentifierClause As SecurityKeyIdentifierClause) As Boolean
        If keyIdentifierClause Is Nothing Then
            Throw New ArgumentNullException("keyIdentifierClause")
        End If

        Return MyBase.MatchesKeyIdentifierClause(keyIdentifierClause)
    End Function

#Region "membros privados"

    Private Function CreateSymmetricSecurityKeys(keys As IEnumerable(Of Byte())) As List(Of SecurityKey)
        Dim symmetricKeys As New List(Of SecurityKey)()
        For Each key As Byte() In keys
            symmetricKeys.Add(New InMemorySymmetricSecurityKey(key))
        Next
        Return symmetricKeys
    End Function

    Private m_id As String
    Private m_effectiveTime As DateTime
    Private m_securityKeys As List(Of SecurityKey)

#End Region
End Class

''' <summary>
''' Representa uma resposta OAuth de uma chamada ao servidor ACS.
''' </summary>
Public Class OAuthTokenResponse

    ''' <summary>
    ''' Construtor padrão.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Constrói um objeto OAuthTokenResponse usando uma matriz de bytes retornada do servidor ACS.
    ''' </summary>
    ''' <param name="response">A matriz de bytes bruta obtida do ACS.</param>
    Public Sub New(ByVal response As Byte())
        Dim serializer = New JavaScriptSerializer()
        Me.Data = TryCast(serializer.DeserializeObject(Encoding.UTF8.GetString(response)), Dictionary(Of String, Object))
        Me.AccessToken = Me.GetValue("access_token")
        Me.TokenType = Me.GetValue("token_type")
        Me.Resource = Me.GetValue("resource")
        Me.UserType = Me.GetValue("user_type")
        Dim epochTime As Long = 0

        If Long.TryParse(Me.GetValue("expires_in"), epochTime) Then
            Me.ExpiresIn = epochTime
        End If

        If Long.TryParse(Me.GetValue("expires_on"), epochTime) Then
            Me.ExpiresOn = epochTime
        End If

        If Long.TryParse(Me.GetValue("not_before"), epochTime) Then
            Me.NotBefore = epochTime
        End If

        If Long.TryParse(Me.GetValue("extended_expires_in"), epochTime) Then
            Me.ExtendedExpiresIn = epochTime
        End If
    End Sub

    ''' <summary>
    ''' Obtém o token de acesso.
    ''' </summary>
    Public Property AccessToken As String

    ''' <summary>
    ''' Obtém os dados analisados da resposta bruta.
    ''' </summary>
    Public ReadOnly Property Data As IDictionary(Of String, Object)

    ''' <summary>
    ''' Obtém o horário da Época de expires in.
    ''' </summary>
    Public ReadOnly Property ExpiresIn As Long

    ''' <summary>
    ''' Obtém as expirações no tempo da Época.
    ''' </summary>
    Public ReadOnly Property ExpiresOn As Long

    ''' <summary>
    ''' Obtém o estendido, que expira no horário da Época.
    ''' </summary>
    Public ReadOnly Property ExtendedExpiresIn As Long

    ''' <summary>
    ''' Obtém as expirações que não estão antes do tempo da Época.
    ''' </summary>
    Public ReadOnly Property NotBefore As Long

    ''' <summary>
    ''' Obtém o recurso.
    ''' </summary>
    Public ReadOnly Property Resource As String

    ''' <summary>
    ''' Obtém o tipo de token.
    ''' </summary>
    Public ReadOnly Property TokenType As String

    ''' <summary>
    ''' Obtém o tipo de usuário.
    ''' </summary>
    Public ReadOnly Property UserType As String

    ''' <summary>
    ''' Obtém um valor dos Dados pela chave.
    ''' </summary>
    ''' <param name="key">A chave.</param>
    ''' <returns>O valor da chave, se existir, caso contrário, uma cadeia de caracteres vazia.</returns>
    Private Function GetValue(ByVal key As String) As String
        Dim value As Object = Nothing

        If Me.Data.TryGetValue(key, value) Then
            Return TryCast(value, String)
        Else
            Return String.Empty
        End If
    End Function
End Class

''' <summary>
''' Representa um cliente Web para fazer uma chamada OAuth ao servidor ACS.
''' </summary>
Public Class OAuthClient

    ''' <summary>
    ''' Obtém um OAuthTokenResponse com um token de atualização.
    ''' </summary>
    ''' <param name="uri">O URI do servidor ACS.</param>
    ''' <param name="clientId">ID do cliente.</param>
    ''' <param name="ClientSecret">Segredo do cliente.</param>
    ''' <param name="refreshToken">Token de atualização.</param>
    ''' <param name="resource">Recurso.</param>
    ''' <returns>Resposta do servidor ACS.</returns>
    Public Shared Function GetAccessTokenWithRefreshToken(ByVal uri As String, ByVal clientId As String, ByVal ClientSecret As String, ByVal refreshToken As String, ByVal resource As String) As OAuthTokenResponse
        Dim client As WebClient = New WebClient()
        Dim values As NameValueCollection = New NameValueCollection From {
            {"grant_type", "refresh_token"},
            {"client_id", clientId},
            {"client_secret", ClientSecret},
            {"refresh_token", refreshToken},
            {"resource", resource}
        }
        Dim response As Byte() = client.UploadValues(uri, "POST", values)
        Return New OAuthTokenResponse(response)
    End Function

    ''' <summary>
    ''' Obtém um OAuthTokenResponse com as credenciais do cliente.
    ''' </summary>
    ''' <param name="uri">O URI do servidor ACS.</param>
    ''' <param name="clientId">ID do cliente.</param>
    ''' <param name="ClientSecret">Segredo do cliente.</param>
    ''' <param name="resource">Recurso.</param>
    ''' <returns>Resposta do servidor ACS.</returns>
    Public Shared Function GetAccessTokenWithClientCredentials(ByVal uri As String, ByVal clientId As String, ByVal ClientSecret As String, ByVal resource As String) As OAuthTokenResponse
        Dim client As WebClient = New WebClient()
        Dim values As NameValueCollection = New NameValueCollection From {
            {"grant_type", "client_credentials"},
            {"client_id", clientId},
            {"client_secret", ClientSecret},
            {"resource", resource}
        }
        Dim response As Byte() = client.UploadValues(uri, "POST", values)
        Return New OAuthTokenResponse(response)
    End Function

    ''' <summary>
    ''' Obtém um OAuthTokenResponse com um código de autorização.
    ''' </summary>
    ''' <param name="uri">O URI do servidor ACS.</param>
    ''' <param name="clientId">ID do cliente.</param>
    ''' <param name="ClientSecret">Segredo do cliente.</param>
    ''' <param name="authorizationCode">Código de autorização.</param>
    ''' <param name="redirectUri">URI de redirecionamento.</param>
    ''' <param name="resource">Recurso.</param>
    ''' <returns>Resposta do servidor ACS.</returns>
    Public Shared Function GetAccessTokenWithAuthorizationCode(ByVal uri As String, ByVal clientId As String, ByVal ClientSecret As String, ByVal authorizationCode As String, ByVal redirectUri As String, ByVal resource As String) As OAuthTokenResponse
        Dim client As WebClient = New WebClient()
        Dim values As NameValueCollection = New NameValueCollection From {
            {"grant_type", "authorization_code"},
            {"client_id", clientId},
            {"client_secret", ClientSecret},
            {"code", authorizationCode},
            {"redirect_uri", redirectUri},
            {"resource", resource}
        }
        Dim response As Byte() = client.UploadValues(uri, "POST", values)
        Return New OAuthTokenResponse(response)
    End Function
End Class
