using Microsoft.Extensions.Caching.Distributed;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class LogoutService : ILogoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IDistributedCache _cache;

        public LogoutService(IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public async Task Desloga()
        {
            await _cache.RemoveAsync("jwt");
        }
    }
}
