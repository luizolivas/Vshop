using KeyCloak.Auth.API.Entities;
using System.Text.Json;

namespace KeyCloak.Auth.API.Services
{
    public class UserService
    {

        public async Task<string> Authenticate(string username, string password)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/realms/mordor/protocol/openid-connect/token");
            var formurlencoded = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "mordor-api" },
                { "username", "froto" },
                { "password", "123" },
                { "client_secret", "VTWI3jx14YCcOF23sLaZ09IYTzKo8Wrw" },
            };
            request.Content = new FormUrlEncodedContent(formurlencoded);
            var response = await client.SendAsync(request).Result.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<AccessToken>(response);

            return token.Token;

        }
    }
}
