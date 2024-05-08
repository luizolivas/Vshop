using KeyCloak.Auth.API.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace KeyCloak.Auth.API.Services
{
    public class KeycloakService
    {
        private readonly HttpClient _httpClient;
        private readonly string _keycloakBaseUrl;
        private readonly string _realm;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public KeycloakService(HttpClient httpClient, string keycloakBaseUrl, string realm, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _keycloakBaseUrl = keycloakBaseUrl;
            _realm = realm;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<TokenResponse> ExchangeCodeForToken(string authorizationCode)
        {
            var tokenRequestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("redirect_uri", "http://your-redirect-uri")
            });

            var tokenResponse = await _httpClient.PostAsync($"{_keycloakBaseUrl}/realms/{_realm}/protocol/openid-connect/token", tokenRequestContent);
            tokenResponse.EnsureSuccessStatusCode();

            var tokenResponseBody = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseObj = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseBody);

            return tokenResponseObj;
        }
    }
}
