using KeyCloakAuth.Entities;

namespace KeyCloakAuth.Services.Contracts
{
    public interface ICallbackKeycloakService
    {
        Task ExchangeCodeForTokens(string session_state, string code);

        public void RetryCode(string code);
    }
}
