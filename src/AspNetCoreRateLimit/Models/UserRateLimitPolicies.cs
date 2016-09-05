using System.Collections.Generic;

namespace AspNetCoreRateLimit
{
    public class UserRateLimitPolicies
    {
        public List<UserRateLimitPolicy> UserRules { get; set; }
    }
}