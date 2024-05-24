using KeyCloakAuth.Entities;
using KeyCloakAuth.Services.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace KeyCloakAuth.Services
{
    public class CallbackKeycloakService : ICallbackKeycloakService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IDistributedCache _cache;

        public CallbackKeycloakService(IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public async Task ExchangeCodeForTokens(string session_state, string code)
        {
            try
            {
                
                string clientId = "caelid-api";
                string clientSecret = "zgFLUdOjq0WCrvDdysialRxinrE2HThg";
                string keycloakBaseUrl = "http://localhost:8080/realms/caelid";

                // Construir a URL do endpoint de token do Keycloak
                var tokenEndpoint = $"{keycloakBaseUrl}/protocol/openid-connect/token";

                // Montar os parâmetros da solicitação
                var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "code", code },
                    { "redirect_uri", "https://localhost:7051/api/CallbackKeycloak" } // Adicione o URI de redirecionamento, se necessário
                };

                //Fazer solicitação POST para trocar o código de autorização por tokens de acesso
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));
                    if (response.IsSuccessStatusCode)
                    {
                        var cacheKey = "jwt";
                        // Ler e desserializar a resposta
                        var content = await response.Content.ReadAsStringAsync();
                        var tokens = JsonSerializer.Deserialize<TokenResponse>(content);
                        await _cache.SetStringAsync(cacheKey,tokens.access_token);
                        //HttpContext.Session.SetString("AuthorizationCode", code);
                        //return tokens;
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

        public void RetryCode(string code)
        {
            var state = Guid.NewGuid().ToString("N");

            string keycloakBaseUrl = "http://localhost:8080/realms/caelid";
            string clientId = "caelid-api";

            //_httpContextAccessor.HttpContext.Session.SetString("State", state);

            // Construir o URL de login do Keycloak
            var keycloakLoginUrl = $"{keycloakBaseUrl}/protocol/openid-connect/auth" +
                $"?client_id={clientId}" +
                "&response_type=code";

            // Adicione a URI de redirecionamento à URL de login
            var redirectUri = "https://localhost:7051/api/CallbackKeycloak";
            keycloakLoginUrl += $"&redirect_uri={redirectUri}";

            // Redirecionar o usuário para a página de login do Keycloak
            _httpContextAccessor.HttpContext.Response.Redirect(keycloakLoginUrl);
        }
    }


}
