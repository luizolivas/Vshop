using Keycloak.AuthServices.Sdk.Admin;

namespace KeyCloakAuth.Services
{
    public class LoginService
    {
        private readonly KeycloakClient _keycloakClient;

        public LoginService(KeycloakClient keycloakClient)
        {
            _keycloakClient = keycloakClient;
        }

        public async Task<LoginResult> Authenticate(string username, string password)
        {
            // Chamar o método de autenticação do KeycloakClient
            var result = await _keycloakClient.AuthenticateAsync(username, password);

            // Verificar o resultado da autenticação
            if (result.Success)
            {
                // Autenticação bem-sucedida
                return new LoginResult { Success = true, AccessToken = result.AccessToken };
            }
            else
            {
                // Autenticação falhou
                return new LoginResult { Success = false, ErrorMessage = result.ErrorMessage };
            }
        }
    }
}
