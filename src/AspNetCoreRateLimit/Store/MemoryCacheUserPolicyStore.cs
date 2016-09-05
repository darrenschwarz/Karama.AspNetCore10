using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AspNetCoreRateLimit
{
    public class MemoryCacheUserPolicyStore : IUserPolicyStore
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheUserPolicyStore(IMemoryCache memoryCache,
            IOptions<UserRateLimitOptions> options = null,
            IOptions<UserRateLimitPolicies> policies = null)
        {
            _memoryCache = memoryCache;

            //save client rules defined in appsettings in cache on startup
            if (options != null && options.Value != null && policies != null && policies.Value != null && policies.Value.UserRules != null)
            {
                foreach (var rule in policies.Value.UserRules)
                {
                    Set($"{options.Value.UserPolicyPrefix}", new UserRateLimitPolicy { Name = rule.Name, Rules = rule.Rules });
                }
            }
        }

        public void Set(string id, UserRateLimitPolicy policy)
        {
            _memoryCache.Set(id, policy);
        }

        public bool Exists(string id)
        {
            UserRateLimitPolicy stored;
            return _memoryCache.TryGetValue(id, out stored);
        }

        public UserRateLimitPolicy Get(string id)
        {
            UserRateLimitPolicy stored;
            if (_memoryCache.TryGetValue(id, out stored))
            {
                return stored;
            }

            return null;
        }

        public void Remove(string id)
        {
            _memoryCache.Remove(id);
        }
    }
}