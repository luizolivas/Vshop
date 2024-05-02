using Microsoft.AspNetCore.Http;


namespace VShop.Web.Services
{
    public class LoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _keycloakBaseUrl;
        private readonly string _clientId;
        private readonly string _redirectUri;

        public LoginService(IHttpContextAccessor httpContextAccessor, string keycloakBaseUrl, string clientId, string redirectUri)
        {
            _httpContextAccessor = httpContextAccessor;
            _keycloakBaseUrl = keycloakBaseUrl;
            _clientId = clientId;
            _redirectUri = redirectUri;
        }

        // Método para iniciar o fluxo de autorização com Keycloak
        public void StartAuthorizationFlow()
        {
            // Construir o URL de login do Keycloak
            var keycloakLoginUrl = $"{_keycloakBaseUrl}/protocol/openid-connect/auth" +
                $"?client_id={_clientId}" +
                $"&redirect_uri={_redirectUri}" +
                "&response_type=code" +
                "&scope=openid";

            // Redirecionar o usuário para a página de login do Keycloak
            _httpContextAccessor.HttpContext.Response.Redirect(keycloakLoginUrl);
        }
    }
}
