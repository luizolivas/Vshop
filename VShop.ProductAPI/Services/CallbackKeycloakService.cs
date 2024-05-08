
namespace VShop.ProductAPI.Services
{
    public class CallbackKeycloakService : ICallbackKeycloakService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _keycloakBaseUrl;
        private readonly string _clientId;
        private readonly string _clientSecret; 

        public CallbackKeycloakService(IHttpContextAccessor httpContextAccessor, string keycloakBaseUrl, string clientId, string clientSecret)
        {
            _httpContextAccessor = httpContextAccessor;
            _keycloakBaseUrl = keycloakBaseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task ExchangeCodeForTokens(string code)
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
                    { "redirect_uri", "https://localhost:7097/CallbackKeycloak" } // Adicione o URI de redirecionamento, se necessário
                };

                // Fazer solicitação POST para trocar o código de autorização por tokens de acesso
                //using (var client = new HttpClient())
                //{
                //    var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(parameters));
                //    if (response.IsSuccessStatusCode)
                //    {
                //        // Ler e desserializar a resposta
                //        var content = await response.Content.ReadAsStringAsync();
                //        var tokens = JsonSerializer.Deserialize<TokenResponse>(content);
                //        return tokens;
                //    }
                //    else
                //    {
                //        // Lidar com erros de solicitação
                //        throw new Exception($"Failed to exchange code for tokens: {response.ReasonPhrase}");
                //    }
                //}
            }
            catch (Exception ex)
            {
                // Lidar com exceções
                throw new Exception("Failed to exchange code for tokens", ex);
            }
        }
    }
}
