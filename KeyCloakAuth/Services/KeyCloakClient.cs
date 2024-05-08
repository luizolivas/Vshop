using System.Net.Http;
using System.Threading.Tasks;

namespace KeyCloakAuth.Services
{
    public class KeycloakClient
    {
        private readonly HttpClient _httpClient;

        public KeycloakClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResult> AuthenticateAsync(string username, string password)
        {
            // Faça uma chamada à API REST do Keycloak para autenticar o usuário
            // Você precisará construir a URL correta e enviar uma solicitação POST com as credenciais do usuário
            // Aqui está um exemplo simplificado:

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/realms/caelid/protocol/openid-connect/token");

            // Adicione as credenciais do usuário ao corpo da solicitação
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });
            request.Content = formData;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Autenticação bem-sucedida, leia o token de acesso do corpo da resposta e retorne-o
                var accessToken = await response.Content.ReadAsStringAsync();
                return new LoginResult { Success = true, AccessToken = accessToken };
            }
            else
            {
                // Autenticação falhou, leia a mensagem de erro da resposta e retorne-a
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new LoginResult { Success = false, ErrorMessage = errorMessage };
            }
        }
    }
}
