using KeyCloak.Auth.API.Entities;


namespace KeyCloak.Auth.API.Services
{
    public interface IKeycloakService
    {
        Task<TokenResponse> Login(string username, string password);
        Task<UserInfoResponse> GetUserInfo(string accessToken);
    }
}
