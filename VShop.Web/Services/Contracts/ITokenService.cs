namespace VShop.Web.Services.Contracts
{
    public interface ITokenService
    {
        Task<bool> IsTokenValid();
    }
}
