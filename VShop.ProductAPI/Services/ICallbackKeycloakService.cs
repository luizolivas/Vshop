

namespace VShop.ProductAPI.Services
{
    public interface ICallbackKeycloakService
    {
        Task ExchangeCodeForTokens(string code);
    }
}
