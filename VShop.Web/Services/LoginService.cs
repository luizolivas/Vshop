using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace VShop.Web.Services
{
    public class LoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _keycloakBaseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret; // Se você tiver um segredo de cliente

        public LoginService(IHttpContextAccessor httpContextAccessor, string keycloakBaseUrl, string clientId, string clientSecret)
        {
            _httpContextAccessor = httpContextAccessor;
            _keycloakBaseUrl = keycloakBaseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        // Método para iniciar o fluxo de autorização com Keycloak
        public void StartAuthorizationFlow()
        {
            // Gere um valor aleatório para o parâmetro de estado
            var state = Guid.NewGuid().ToString("N");

            // Armazene o valor do parâmetro de estado na sessão ou em outro local de armazenamento seguro
            _httpContextAccessor.HttpContext.Session.SetString("State", state);

            // Construir o URL de login do Keycloak
            var keycloakLoginUrl = $"{_keycloakBaseUrl}/protocol/openid-connect/auth" +
                $"?client_id={_clientId}" +
                "&response_type=code" +
                "&scope=openid" +
                $"&state={state}";

            // Adicione a URI de redirecionamento à URL de login
            var redirectUri = "https://localhost:7097/CallbackKeycloak";
            keycloakLoginUrl += $"&redirect_uri={redirectUri}";

            // Redirecionar o usuário para a página de login do Keycloak
            _httpContextAccessor.HttpContext.Response.Redirect(keycloakLoginUrl);
        }

        // Método para trocar o código de autorização por tokens de acesso
        public async Task<TokenResponse> ExchangeCodeForTokens(string code)
        {
            try
            {
                // Construir a URL do endpoint de token do Keycloak
                var tokenEndpoint = $"{_keycloakBaseUrl}/protocol/openid-connect/token";

                // Montar os parâmetros da solicitação
                var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "client_id", _clientId },
                    { "code", code },
                    { "redirect_uri", "https://localhost:7097/Login/KeycloakCallback" } // Adicione o URI de redirecionamento, se necessário
                };

                // Fazer solicitação POST para trocar o código de autorização por tokens de acesso
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));
                    if (response.IsSuccessStatusCode)
                    {
                        // Ler e desserializar a resposta
                        var content = await response.Content.ReadAsStringAsync();
                        var tokens = JsonSerializer.Deserialize<TokenResponse>(content);
                        return tokens;
                    }
                    else
                    {
                        // Lidar com erros de solicitação
                        throw new Exception($"Failed to exchange code for tokens: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções
                throw new Exception("Failed to exchange code for tokens", ex);
            }
        }
    }
}
