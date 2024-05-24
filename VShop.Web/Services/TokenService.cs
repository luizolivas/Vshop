using Microsoft.Extensions.Caching.Distributed;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class TokenService : ITokenService
    {
        private readonly IDistributedCache _cache;

        public TokenService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> IsTokenValid()
        {
            var cacheKey = "jwt";
            var token = await _cache.GetStringAsync(cacheKey);
            return !string.IsNullOrEmpty(token);
        }
    }
}
