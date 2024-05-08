namespace KeyCloakAuth.Services
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public string AccessToken { get; set; }
    }
}
