Imports System.IdentityModel.Tokens
Imports System.Net
Imports System.Security.Principal
Imports Microsoft.SharePoint.Client

''' <summary>
''' Encapsula todas as informações do SharePoint.
''' </summary>
Public MustInherit Class SharePointContext
    Public Const SPHostUrlKey As String = "SPHostUrl"
    Public Const SPAppWebUrlKey As String = "SPAppWebUrl"
    Public Const SPLanguageKey As String = "SPLanguage"
    Public Const SPClientTagKey As String = "SPClientTag"
    Public Const SPProductNumberKey As String = "SPProductNumber"

    Protected Shared ReadOnly AccessTokenLifetimeTolerance As Long = 5 * 60 '5 minutos

    Private ReadOnly m_spHostUrl As Uri
    Private ReadOnly m_spAppWebUrl As Uri
    Private ReadOnly m_spLanguage As String
    Private ReadOnly m_spClientTag As String
    Private ReadOnly m_spProductNumber As String

    ' <AccessTokenString, UtcExpiresOn no horário da Época>
    Protected m_userAccessTokenForSPHost As Tuple(Of String, Long)
    Protected m_userAccessTokenForSPAppWeb As Tuple(Of String, Long)
    Protected m_appOnlyAccessTokenForSPHost As Tuple(Of String, Long)
    Protected m_appOnlyAccessTokenForSPAppWeb As Tuple(Of String, Long)

    ''' <summary>
    ''' Obtém a URL do host do SharePoint da QueryString da solicitação HTTP especificada.
    ''' </summary>
    ''' <param name="httpRequest">A solicitação HTTP especificada.</param>
    ''' <returns>A URL do host do SharePoint. Retornará <c>Nada</c> se a solicitação HTTP não contiver a URL do host do SharePoint.</returns>
    Public Shared Function GetSPHostUrl(httpRequest As HttpRequestBase) As Uri
        If httpRequest Is Nothing Then
            Throw New ArgumentNullException("httpRequest")
        End If

        Dim spHostUrlString As String = TokenHelper.EnsureTrailingSlash(httpRequest.QueryString(SPHostUrlKey))
        Dim spHostUrl As Uri = Nothing
        If Uri.TryCreate(spHostUrlString, UriKind.Absolute, spHostUrl) AndAlso
           (spHostUrl.Scheme = Uri.UriSchemeHttp OrElse spHostUrl.Scheme = Uri.UriSchemeHttps) Then
            Return spHostUrl
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Obtém a URL do host do SharePoint da QueryString da solicitação HTTP especificada.
    ''' </summary>
    ''' <param name="httpRequest">A solicitação HTTP especificada.</param>
    ''' <returns>A URL do host do SharePoint. Retornará <c>Nada</c> se a solicitação HTTP não contiver a URL do host do SharePoint.</returns>
    Public Shared Function GetSPHostUrl(httpRequest As HttpRequest) As Uri
        Return GetSPHostUrl(New HttpRequestWrapper(httpRequest))
    End Function

    ''' <summary>
    ''' A URL do host do SharePoint.
    ''' </summary>
    Public ReadOnly Property SPHostUrl() As Uri
        Get
            Return Me.m_spHostUrl
        End Get
    End Property

    ''' <summary>
    ''' A URL da Web do aplicativo SharePoint.
    ''' </summary>
    Public ReadOnly Property SPAppWebUrl() As Uri
        Get
            Return Me.m_spAppWebUrl
        End Get
    End Property

    ''' <summary>
    ''' O idioma do SharePoint.
    ''' </summary>
    Public ReadOnly Property SPLanguage() As String
        Get
            Return Me.m_spLanguage
        End Get
    End Property

    ''' <summary>
    ''' A marca de cliente do SharePoint.
    ''' </summary>
    Public ReadOnly Property SPClientTag() As String
        Get
            Return Me.m_spClientTag
        End Get
    End Property

    ''' <summary>
    ''' O número do produto do SharePoint.
    ''' </summary>
    Public ReadOnly Property SPProductNumber() As String
        Get
            Return Me.m_spProductNumber
        End Get
    End Property

    ''' <summary>
    ''' O token de acesso do usuário para o host do SharePoint.
    ''' </summary>
    Public MustOverride ReadOnly Property UserAccessTokenForSPHost() As String

    ''' <summary>
    ''' O token de acesso do usuário para o aplicativo Web do SharePoint.
    ''' </summary>
    Public MustOverride ReadOnly Property UserAccessTokenForSPAppWeb() As String

    ''' <summary>
    ''' O token de acesso somente do aplicativo para o host do SharePoint.
    ''' </summary>
    Public MustOverride ReadOnly Property AppOnlyAccessTokenForSPHost() As String

    ''' <summary>
    ''' O token de acesso somente do aplicativo para o aplicativo Web do SharePoint.
    ''' </summary>
    Public MustOverride ReadOnly Property AppOnlyAccessTokenForSPAppWeb() As String

    ''' <summary>
    ''' Construtor.
    ''' </summary>
    ''' <param name="spHostUrl">A URL do host do SharePoint.</param>
    ''' <param name="spAppWebUrl">A URL da Web do aplicativo SharePoint.</param>
    ''' <param name="spLanguage">O idioma do SharePoint.</param>
    ''' <param name="spClientTag">A marca de cliente do SharePoint.</param>
    ''' <param name="spProductNumber">O número do produto do SharePoint.</param>
    Protected Sub New(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String)
        If spHostUrl Is Nothing Then
            Throw New ArgumentNullException("spHostUrl")
        End If

        If String.IsNullOrEmpty(spLanguage) Then
            Throw New ArgumentNullException("spLanguage")
        End If

        If String.IsNullOrEmpty(spClientTag) Then
            Throw New ArgumentNullException("spClientTag")
        End If

        If String.IsNullOrEmpty(spProductNumber) Then
            Throw New ArgumentNullException("spProductNumber")
        End If

        Me.m_spHostUrl = spHostUrl
        Me.m_spAppWebUrl = spAppWebUrl
        Me.m_spLanguage = spLanguage
        Me.m_spClientTag = spClientTag
        Me.m_spProductNumber = spProductNumber
    End Sub

    ''' <summary>
    ''' Cria um ClientContext do usuário para o host do SharePoint.
    ''' </summary>
    ''' <returns>Uma instância de ClientContext.</returns>
    Public Function CreateUserClientContextForSPHost() As ClientContext
        Return CreateClientContext(Me.SPHostUrl, Me.UserAccessTokenForSPHost)
    End Function

    ''' <summary>
    ''' Cria um ClientContext do usuário para o aplicativo Web do SharePoint.
    ''' </summary>
    ''' <returns>Uma instância de ClientContext.</returns>
    Public Function CreateUserClientContextForSPAppWeb() As ClientContext
        Return CreateClientContext(Me.SPAppWebUrl, Me.UserAccessTokenForSPAppWeb)
    End Function

    ''' <summary>
    ''' Cria um ClientContext somente do aplicativo para o host do SharePoint.
    ''' </summary>
    ''' <returns>Uma instância de ClientContext.</returns>
    Public Function CreateAppOnlyClientContextForSPHost() As ClientContext
        Return CreateClientContext(Me.SPHostUrl, Me.AppOnlyAccessTokenForSPHost)
    End Function

    ''' <summary>
    ''' Cria um ClientContext somente do aplicativo para o aplicativo Web do SharePoint.
    ''' </summary>
    ''' <returns>Uma instância de ClientContext.</returns>
    Public Function CreateAppOnlyClientContextForSPAppWeb() As ClientContext
        Return CreateClientContext(Me.SPAppWebUrl, Me.AppOnlyAccessTokenForSPAppWeb)
    End Function

    ''' <summary>
    ''' Obtém a cadeia de conexão de banco de dados do SharePoint para o suplemento auto-hospedado.
    ''' Esse método é preterido porque a opção auto-hospedado não está mais disponível.
    ''' </summary>
    <ObsoleteAttribute("This method is deprecated because the autohosted option is no longer available.", true)>
    Public Function GetDatabaseConnectionString() As String
        Throw New NotSupportedException("This method is deprecated because the autohosted option is no longer available.")
    End Function

    ''' <summary>
    ''' Determina se o token de acesso especificado é válido.
    ''' Ele considera um token de acesso como não é válido se ele seu valor for Nada ou ele tiver expirado.
    ''' </summary>
    ''' <param name="accessToken">O token de acesso a verificar.</param>
    ''' <returns>True se o token de acesso for válido.</returns>
    Protected Shared Function IsAccessTokenValid(accessToken As Tuple(Of String, Long)) As Boolean
        Return accessToken IsNot Nothing AndAlso
               Not String.IsNullOrEmpty(accessToken.Item1) AndAlso
               accessToken.Item2 > TokenHelper.EpochTimeNow()
    End Function

    ''' <summary>
    ''' Cria um ClientContext com a URL do site do SharePoint especificada e o token de acesso.
    ''' </summary>
    ''' <param name="spSiteUrl">a URL do site.</param>
    ''' <param name="accessToken">O token de acesso.</param>
    ''' <returns>Uma instância de ClientContext.</returns>
    Private Shared Function CreateClientContext(spSiteUrl As Uri, accessToken As String) As ClientContext
        If spSiteUrl IsNot Nothing AndAlso Not String.IsNullOrEmpty(accessToken) Then
            Return TokenHelper.GetClientContextWithAccessToken(spSiteUrl.AbsoluteUri, accessToken)
        End If

        Return Nothing
    End Function
End Class

''' <summary>
''' Status de redirecionamento.
''' </summary>
Public Enum RedirectionStatus
    Ok
    ShouldRedirect
    CanNotRedirect
End Enum

''' <summary>
''' Fornece instâncias de SharePointContext.
''' </summary>
Public MustInherit Class SharePointContextProvider
    Private Shared s_current As SharePointContextProvider

    ''' <summary>
    ''' A instância de SharePointContextProvider atual.
    ''' </summary>
    Public Shared ReadOnly Property Current() As SharePointContextProvider
        Get
            Return SharePointContextProvider.s_current
        End Get
    End Property

    ''' <summary>
    ''' Inicializa a instância de SharePointContextProvider padrão.
    ''' </summary>
    Shared Sub New()
        If Not TokenHelper.IsHighTrustApp() Then
            SharePointContextProvider.s_current = New SharePointAcsContextProvider()
        Else
            SharePointContextProvider.s_current = New SharePointHighTrustContextProvider()
        End If
    End Sub

    ''' <summary>
    ''' Registra a instância SharePointContextProvider especificada como atual.
    ''' Ela deve ser chamada por Application_Start() em Global.asax.
    ''' </summary>
    ''' <param name="provider">O SharePointContextProvider a definir como atual.</param>
    Public Shared Sub Register(provider As SharePointContextProvider)
        If provider Is Nothing Then
            Throw New ArgumentNullException("provider")
        End If

        SharePointContextProvider.s_current = provider
    End Sub

    ''' <summary>
    ''' Verifica se é necessário redirecionar para o SharePoint para que o usuário se autentique.
    ''' </summary>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <param name="redirectUrl">A URL de redirecionamento para o SharePoint se o status for ShouldRedirect. <c>Nulo</c> se o status for Ok ou CanNotRedirect.</param>
    ''' <returns>Status de redirecionamento.</returns>
    Public Shared Function CheckRedirectionStatus(httpContext As HttpContextBase, ByRef redirectUrl As Uri) As RedirectionStatus
        If httpContext Is Nothing Then
            Throw New ArgumentNullException("httpContext")
        End If

        redirectUrl = Nothing
        Dim contextTokenExpired As Boolean = False

        Try
            If SharePointContextProvider.Current.GetSharePointContext(httpContext) IsNot Nothing Then
                Return RedirectionStatus.Ok
            End If
        Catch ex As SecurityTokenExpiredException
            contextTokenExpired = True
        End Try

        Const SPHasRedirectedToSharePointKey As String = "SPHasRedirectedToSharePoint"

        If Not String.IsNullOrEmpty(httpContext.Request.QueryString(SPHasRedirectedToSharePointKey)) AndAlso Not contextTokenExpired Then
            Return RedirectionStatus.CanNotRedirect
        End If

        Dim spHostUrl As Uri = SharePointContext.GetSPHostUrl(httpContext.Request)

        If spHostUrl Is Nothing Then
            Return RedirectionStatus.CanNotRedirect
        End If

        If StringComparer.OrdinalIgnoreCase.Equals(httpContext.Request.HttpMethod, "POST") Then
            Return RedirectionStatus.CanNotRedirect
        End If

        Dim requestUrl As Uri = httpContext.Request.Url

        Dim queryNameValueCollection = HttpUtility.ParseQueryString(requestUrl.Query)

        ' Remove os valores que estão incluídos em {StandardTokens}, pois {StandardTokens} será inserido no início da cadeia de caracteres de consulta.
        queryNameValueCollection.Remove(SharePointContext.SPHostUrlKey)
        queryNameValueCollection.Remove(SharePointContext.SPAppWebUrlKey)
        queryNameValueCollection.Remove(SharePointContext.SPLanguageKey)
        queryNameValueCollection.Remove(SharePointContext.SPClientTagKey)
        queryNameValueCollection.Remove(SharePointContext.SPProductNumberKey)

        ' Adiciona SPHasRedirectedToSharePoint=1.
        queryNameValueCollection.Add(SPHasRedirectedToSharePointKey, "1")

        Dim returnUrlBuilder As New UriBuilder(requestUrl)
        returnUrlBuilder.Query = queryNameValueCollection.ToString()

        ' Insere StandardTokens.
        Const StandardTokens As String = "{StandardTokens}"
        Dim returnUrlString As String = returnUrlBuilder.Uri.AbsoluteUri
        returnUrlString = returnUrlString.Insert(returnUrlString.IndexOf("?") + 1, StandardTokens + "&")

        ' Constrói a URL de redirecionamento.
        Dim redirectUrlString As String = TokenHelper.GetAppContextTokenRequestUrl(spHostUrl.AbsoluteUri, Uri.EscapeDataString(returnUrlString))

        redirectUrl = New Uri(redirectUrlString, UriKind.Absolute)

        Return RedirectionStatus.ShouldRedirect
    End Function

    ''' <summary>
    ''' Verifica se é necessário redirecionar para o SharePoint para que o usuário se autentique.
    ''' </summary>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <param name="redirectUrl">A URL de redirecionamento para o SharePoint se o status for ShouldRedirect. <c>Nulo</c> se o status for Ok ou CanNotRedirect.</param>
    ''' <returns>Status de redirecionamento.</returns>
    Public Shared Function CheckRedirectionStatus(httpContext As HttpContext, ByRef redirectUrl As Uri) As RedirectionStatus
        Return CheckRedirectionStatus(New HttpContextWrapper(httpContext), redirectUrl)
    End Function

    ''' <summary>
    ''' Cria uma instância do SharePointContext com a solicitação HTTP especificada.
    ''' </summary>
    ''' <param name="httpRequest">A solicitação HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se ocorrerem erros.</returns>
    Public Function CreateSharePointContext(httpRequest As HttpRequestBase) As SharePointContext
        If httpRequest Is Nothing Then
            Throw New ArgumentNullException("httpRequest")
        End If

        ' SPHostUrl
        Dim spHostUrl As Uri = SharePointContext.GetSPHostUrl(httpRequest)
        If spHostUrl Is Nothing Then
            Return Nothing
        End If

        ' SPAppWebUrl
        Dim spAppWebUrlString As String = TokenHelper.EnsureTrailingSlash(httpRequest.QueryString(SharePointContext.SPAppWebUrlKey))
        Dim spAppWebUrl As Uri = Nothing
        If Not Uri.TryCreate(spAppWebUrlString, UriKind.Absolute, spAppWebUrl) OrElse
           Not (spAppWebUrl.Scheme = Uri.UriSchemeHttp OrElse spAppWebUrl.Scheme = Uri.UriSchemeHttps) Then
            spAppWebUrl = Nothing
        End If

        ' SPLanguage
        Dim spLanguage As String = httpRequest.QueryString(SharePointContext.SPLanguageKey)
        If String.IsNullOrEmpty(spLanguage) Then
            Return Nothing
        End If

        ' SPClientTag
        Dim spClientTag As String = httpRequest.QueryString(SharePointContext.SPClientTagKey)
        If String.IsNullOrEmpty(spClientTag) Then
            Return Nothing
        End If

        ' SPProductNumber
        Dim spProductNumber As String = httpRequest.QueryString(SharePointContext.SPProductNumberKey)
        If String.IsNullOrEmpty(spProductNumber) Then
            Return Nothing
        End If

        Return CreateSharePointContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, httpRequest)
    End Function

    ''' <summary>
    ''' Cria uma instância do SharePointContext com a solicitação HTTP especificada.
    ''' </summary>
    ''' <param name="httpRequest">A solicitação HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se ocorrerem erros.</returns>
    Public Function CreateSharePointContext(httpRequest As HttpRequest) As SharePointContext
        Return CreateSharePointContext(New HttpRequestWrapper(httpRequest))
    End Function

    ''' <summary>
    ''' Obtém uma instância do SharePointContext associada ao contexto HTTP especificado.
    ''' </summary>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se não for encontrada e uma nova instância não puder ser criada.</returns>
    Public Function GetSharePointContext(httpContext As HttpContextBase) As SharePointContext
        If httpContext Is Nothing Then
            Throw New ArgumentNullException("httpContext")
        End If

        Dim spHostUrl As Uri = SharePointContext.GetSPHostUrl(httpContext.Request)
        If spHostUrl Is Nothing Then
            Return Nothing
        End If

        Dim spContext As SharePointContext = LoadSharePointContext(httpContext)

        If spContext Is Nothing Or Not ValidateSharePointContext(spContext, httpContext) Then
            spContext = CreateSharePointContext(httpContext.Request)

            If spContext IsNot Nothing Then
                SaveSharePointContext(spContext, httpContext)
            End If
        End If

        Return spContext
    End Function

    ''' <summary>
    ''' Obtém uma instância do SharePointContext associada ao contexto HTTP especificado.
    ''' </summary>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se não for encontrada e uma nova instância não puder ser criada.</returns>
    Public Function GetSharePointContext(httpContext As HttpContext) As SharePointContext
        Return GetSharePointContext(New HttpContextWrapper(httpContext))
    End Function

    ''' <summary>
    ''' Cria uma instância de SharePointContext.
    ''' </summary>
    ''' <param name="spHostUrl">A URL do host do SharePoint.</param>
    ''' <param name="spAppWebUrl">A URL da Web do aplicativo SharePoint.</param>
    ''' <param name="spLanguage">O idioma do SharePoint.</param>
    ''' <param name="spClientTag">A marca de cliente do SharePoint.</param>
    ''' <param name="spProductNumber">O número do produto do SharePoint.</param>
    ''' <param name="httpRequest">A solicitação HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se ocorrerem erros.</returns>
    Protected MustOverride Function CreateSharePointContext(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String, httpRequest As HttpRequestBase) As SharePointContext

    ''' <summary>
    ''' Valida se o SharePointContext fornecido pode ser usado com o contexto HTTP especificado.
    ''' </summary>
    ''' <param name="spContext">O SharePointContext.</param>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <returns>True se o SharePointContext fornecido puder ser usado com o contexto HTTP especificado.</returns>
    Protected MustOverride Function ValidateSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase) As Boolean

    ''' <summary>
    ''' Carrega a instância do SharePointContext associada ao contexto HTTP especificado.
    ''' </summary>
    ''' <param name="httpContext">O contexto HTTP.</param>
    ''' <returns>A instância de SharePointContext. Retornará <c>Nada</c> se ela não for encontrada.</returns>
    Protected MustOverride Function LoadSharePointContext(httpContext As HttpContextBase) As SharePointContext

    ''' <summary>
    ''' Salva a instância do SharePointContext especificada associada ao contexto HTTP especificado.
    ''' <c>Nada</c> é aceito para limpar a instância do SharePointContext associada ao contexto HTTP especificado.
    ''' </summary>
    ''' <param name="spContext">A instância de SharePointContext a ser salva ou <c>Nada</c>.</param>
    ''' <param name="httpContext">O contexto HTTP.</param>
    Protected MustOverride Sub SaveSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase)
End Class

#Region "ACS"

''' <summary>
''' Encapsula todas as informações do SharePoint no modo ACS.
''' </summary>
Public Class SharePointAcsContext
    Inherits SharePointContext
    Private ReadOnly m_contextToken As String
    Private ReadOnly m_contextTokenObj As SharePointContextToken

    ''' <summary>
    ''' O token de contexto.
    ''' </summary>
    Public ReadOnly Property ContextToken() As String
        Get
            Return If(Me.m_contextTokenObj.ValidTo > DateTime.UtcNow, Me.m_contextToken, Nothing)
        End Get
    End Property

    ''' <summary>
    ''' A declaração "CacheKey" do token de contexto.
    ''' </summary>
    Public ReadOnly Property CacheKey() As String
        Get
            Return If(Me.m_contextTokenObj.ValidTo > DateTime.UtcNow, Me.m_contextTokenObj.CacheKey, Nothing)
        End Get
    End Property

    ''' <summary>
    ''' A declaração "refreshtoken" do token de contexto.
    ''' </summary>
    Public ReadOnly Property RefreshToken() As String
        Get
            Return If(Me.m_contextTokenObj.ValidTo > DateTime.UtcNow, Me.m_contextTokenObj.RefreshToken, Nothing)
        End Get
    End Property

    Public Overrides ReadOnly Property UserAccessTokenForSPHost() As String
        Get
            Return GetAccessTokenString(Me.m_userAccessTokenForSPHost, Function() TokenHelper.GetAccessToken(Me.m_contextTokenObj, Me.SPHostUrl.Authority))
        End Get
    End Property

    Public Overrides ReadOnly Property UserAccessTokenForSPAppWeb() As String
        Get
            If Me.SPAppWebUrl Is Nothing Then
                Return Nothing
            End If

            Return GetAccessTokenString(Me.m_userAccessTokenForSPAppWeb, Function() TokenHelper.GetAccessToken(Me.m_contextTokenObj, Me.SPAppWebUrl.Authority))
        End Get
    End Property

    Public Overrides ReadOnly Property AppOnlyAccessTokenForSPHost() As String
        Get
            Return GetAccessTokenString(Me.m_appOnlyAccessTokenForSPHost, Function() TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, Me.SPHostUrl.Authority, TokenHelper.GetRealmFromTargetUrl(Me.SPHostUrl)))
        End Get
    End Property

    Public Overrides ReadOnly Property AppOnlyAccessTokenForSPAppWeb() As String
        Get
            If Me.SPAppWebUrl Is Nothing Then
                Return Nothing
            End If

            Return GetAccessTokenString(Me.m_appOnlyAccessTokenForSPAppWeb, Function() TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, Me.SPAppWebUrl.Authority, TokenHelper.GetRealmFromTargetUrl(Me.SPAppWebUrl)))
        End Get
    End Property

    Public Sub New(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String, contextToken As String, contextTokenObj As SharePointContextToken)
        MyBase.New(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber)
        If String.IsNullOrEmpty(contextToken) Then
            Throw New ArgumentNullException("contextToken")
        End If

        If contextTokenObj Is Nothing Then
            Throw New ArgumentNullException("contextTokenObj")
        End If

        Me.m_contextToken = contextToken
        Me.m_contextTokenObj = contextTokenObj
    End Sub

    ''' <summary>
    ''' Garante o token de acesso é válido e o retorna.
    ''' </summary>
    ''' <param name="accessToken">O token de acesso a verificar.</param>
    ''' <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
    ''' <returns>A cadeia de caracteres do token de acesso.</returns>
    Private Shared Function GetAccessTokenString(ByRef accessToken As Tuple(Of String, Long), tokenRenewalHandler As Func(Of OAuthTokenResponse)) As String
        RenewAccessTokenIfNeeded(accessToken, tokenRenewalHandler)

        Return If(IsAccessTokenValid(accessToken), accessToken.Item1, Nothing)
    End Function

    ''' <summary>
    ''' Renova o token de acesso se ele não for válido.
    ''' </summary>
    ''' <param name="accessToken">O token de acesso a renovar.</param>
    ''' <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
    Private Shared Sub RenewAccessTokenIfNeeded(ByRef accessToken As Tuple(Of String, Long), tokenRenewalHandler As Func(Of OAuthTokenResponse))
        If IsAccessTokenValid(accessToken) Then
            Return
        End If

        Try
            Dim oauthTokenResponse As OAuthTokenResponse = tokenRenewalHandler()

            Dim expiresOn As Long = oauthTokenResponse.ExpiresOn

            If (expiresOn - oauthTokenResponse.NotBefore) > AccessTokenLifetimeTolerance Then
                ' Fazer com que o token de acesso seja renovado um pouco mais cedo do que a hora em que ele expirar
                ' para que as chamadas para o SharePoint com ele tenham tempo suficiente para serem concluídas com êxito.
                expiresOn -= AccessTokenLifetimeTolerance
            End If

            accessToken = Tuple.Create(oauthTokenResponse.AccessToken, expiresOn)
        Catch ex As WebException
        End Try
    End Sub
End Class

''' <summary>
''' Provedor padrão para SharePointAcsContext.
''' </summary>
Public Class SharePointAcsContextProvider
    Inherits SharePointContextProvider
    Private Const SPContextKey As String = "SPContext"
    Private Const SPCacheKeyKey As String = "SPCacheKey"

    Protected Overrides Function CreateSharePointContext(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String, httpRequest As HttpRequestBase) As SharePointContext
        Dim contextTokenString As String = TokenHelper.GetContextTokenFromRequest(httpRequest)
        If String.IsNullOrEmpty(contextTokenString) Then
            Return Nothing
        End If

        Dim contextToken As SharePointContextToken = Nothing
        Try
            contextToken = TokenHelper.ReadAndValidateContextToken(contextTokenString, httpRequest.Url.Authority)
        Catch ex As WebException
            Return Nothing
        Catch ex As AudienceUriValidationFailedException
            Return Nothing
        End Try

        Return New SharePointAcsContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, contextTokenString, contextToken)
    End Function

    Protected Overrides Function ValidateSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase) As Boolean
        Dim spAcsContext As SharePointAcsContext = TryCast(spContext, SharePointAcsContext)

        If spAcsContext IsNot Nothing Then
            Dim spHostUrl As Uri = SharePointContext.GetSPHostUrl(httpContext.Request)
            Dim contextToken As String = TokenHelper.GetContextTokenFromRequest(httpContext.Request)
            Dim spCacheKeyCookie As HttpCookie = httpContext.Request.Cookies(SPCacheKeyKey)
            Dim spCacheKey As String = If(spCacheKeyCookie IsNot Nothing, spCacheKeyCookie.Value, Nothing)

            Return spHostUrl = spAcsContext.SPHostUrl AndAlso
                   Not String.IsNullOrEmpty(spAcsContext.CacheKey) AndAlso
                   spCacheKey = spAcsContext.CacheKey AndAlso
                   Not String.IsNullOrEmpty(spAcsContext.ContextToken) AndAlso
                   (String.IsNullOrEmpty(contextToken) OrElse contextToken = spAcsContext.ContextToken)
        End If

        Return False
    End Function

    Protected Overrides Function LoadSharePointContext(httpContext As HttpContextBase) As SharePointContext
        Return TryCast(httpContext.Session(SPContextKey), SharePointAcsContext)
    End Function

    Protected Overrides Sub SaveSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase)
        Dim spAcsContext As SharePointAcsContext = TryCast(spContext, SharePointAcsContext)

        If spAcsContext IsNot Nothing Then
            Dim spCacheKeyCookie As New HttpCookie(SPCacheKeyKey) With
            {
                .Value = spAcsContext.CacheKey,
                .Secure = True,
                .HttpOnly = True
            }

            httpContext.Response.AppendCookie(spCacheKeyCookie)
        End If

        httpContext.Session(SPContextKey) = spAcsContext
    End Sub
End Class

#End Region

#Region "HighTrust"

''' <summary>
''' Encapsula todas as informações do SharePoint no modo HighTrust.
''' </summary>
Public Class SharePointHighTrustContext
    Inherits SharePointContext
    Private ReadOnly m_logonUserIdentity As WindowsIdentity

    ''' <summary>
    ''' A identidade do Windows para o usuário atual.
    ''' </summary>
    Public ReadOnly Property LogonUserIdentity() As WindowsIdentity
        Get
            Return Me.m_logonUserIdentity
        End Get
    End Property

    Public Overrides ReadOnly Property UserAccessTokenForSPHost() As String
        Get
            Return GetAccessTokenString(Me.m_userAccessTokenForSPHost, Function() TokenHelper.GetS2SAccessTokenWithWindowsIdentity(Me.SPHostUrl, Me.LogonUserIdentity))
        End Get
    End Property

    Public Overrides ReadOnly Property UserAccessTokenForSPAppWeb() As String
        Get
            If Me.SPAppWebUrl Is Nothing Then
                Return Nothing
            End If

            Return GetAccessTokenString(Me.m_userAccessTokenForSPAppWeb, Function() TokenHelper.GetS2SAccessTokenWithWindowsIdentity(Me.SPAppWebUrl, Me.LogonUserIdentity))
        End Get
    End Property

    Public Overrides ReadOnly Property AppOnlyAccessTokenForSPHost() As String
        Get
            Return GetAccessTokenString(Me.m_appOnlyAccessTokenForSPHost, Function() TokenHelper.GetS2SAccessTokenWithWindowsIdentity(Me.SPHostUrl, Nothing))
        End Get
    End Property

    Public Overrides ReadOnly Property AppOnlyAccessTokenForSPAppWeb() As String
        Get
            If Me.SPAppWebUrl Is Nothing Then
                Return Nothing
            End If

            Return GetAccessTokenString(Me.m_appOnlyAccessTokenForSPAppWeb, Function() TokenHelper.GetS2SAccessTokenWithWindowsIdentity(Me.SPAppWebUrl, Nothing))
        End Get
    End Property

    Public Sub New(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String, logonUserIdentity As WindowsIdentity)
        MyBase.New(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber)
        If logonUserIdentity Is Nothing Then
            Throw New ArgumentNullException("logonUserIdentity")
        End If

        Me.m_logonUserIdentity = logonUserIdentity
    End Sub

    ''' <summary>
    ''' Garante o token de acesso é válido e o retorna.
    ''' </summary>
    ''' <param name="accessToken">O token de acesso a verificar.</param>
    ''' <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
    ''' <returns>A cadeia de caracteres do token de acesso.</returns>
    Private Shared Function GetAccessTokenString(ByRef accessToken As Tuple(Of String, Long), tokenRenewalHandler As Func(Of String)) As String
        RenewAccessTokenIfNeeded(accessToken, tokenRenewalHandler)

        Return If(IsAccessTokenValid(accessToken), accessToken.Item1, Nothing)
    End Function

    ''' <summary>
    ''' Renova o token de acesso se ele não for válido.
    ''' </summary>
    ''' <param name="accessToken">O token de acesso a renovar.</param>
    ''' <param name="tokenRenewalHandler">O manipulador de renovação do token.</param>
    Private Shared Sub RenewAccessTokenIfNeeded(ByRef accessToken As Tuple(Of String, Long), tokenRenewalHandler As Func(Of String))
        If IsAccessTokenValid(accessToken) Then
            Return
        End If

        Dim expiresOn As Long = TokenHelper.EpochTimeNow() + TokenHelper.HighTrustAccessTokenLifetime.TotalSeconds

        If TokenHelper.HighTrustAccessTokenLifetime.TotalSeconds > AccessTokenLifetimeTolerance Then
            ' Fazer com que o token de acesso seja renovado um pouco mais cedo do que a hora em que ele expirar
            ' para que as chamadas para o SharePoint com ele tenham tempo suficiente para serem concluídas com êxito.
            expiresOn -= AccessTokenLifetimeTolerance
        End If

        accessToken = Tuple.Create(tokenRenewalHandler(), expiresOn)
    End Sub
End Class

''' <summary>
''' Provedor padrão para SharePointHighTrustContext.
''' </summary>
Public Class SharePointHighTrustContextProvider
    Inherits SharePointContextProvider
    Private Const SPContextKey As String = "SPContext"

    Protected Overrides Function CreateSharePointContext(spHostUrl As Uri, spAppWebUrl As Uri, spLanguage As String, spClientTag As String, spProductNumber As String, httpRequest As HttpRequestBase) As SharePointContext
        Dim logonUserIdentity As WindowsIdentity = httpRequest.LogonUserIdentity
        If logonUserIdentity Is Nothing Or Not logonUserIdentity.IsAuthenticated Or logonUserIdentity.IsGuest Or logonUserIdentity.User Is Nothing Then
            Return Nothing
        End If

        Return New SharePointHighTrustContext(spHostUrl, spAppWebUrl, spLanguage, spClientTag, spProductNumber, logonUserIdentity)
    End Function

    Protected Overrides Function ValidateSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase) As Boolean
        Dim spHighTrustContext As SharePointHighTrustContext = TryCast(spContext, SharePointHighTrustContext)

        If spHighTrustContext IsNot Nothing Then
            Dim spHostUrl As Uri = SharePointContext.GetSPHostUrl(httpContext.Request)
            Dim logonUserIdentity As WindowsIdentity = httpContext.Request.LogonUserIdentity

            Return spHostUrl = spHighTrustContext.SPHostUrl AndAlso
                   logonUserIdentity IsNot Nothing AndAlso
                   logonUserIdentity.IsAuthenticated AndAlso
                   Not logonUserIdentity.IsGuest AndAlso
                   logonUserIdentity.User = spHighTrustContext.LogonUserIdentity.User
        End If

        Return False
    End Function

    Protected Overrides Function LoadSharePointContext(httpContext As HttpContextBase) As SharePointContext
        Return TryCast(httpContext.Session(SPContextKey), SharePointHighTrustContext)
    End Function

    Protected Overrides Sub SaveSharePointContext(spContext As SharePointContext, httpContext As HttpContextBase)
        httpContext.Session(SPContextKey) = TryCast(spContext, SharePointHighTrustContext)
    End Sub
End Class

#End Region
