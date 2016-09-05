using System.Collections.Generic;

namespace AspNetCoreRateLimit
{
    public class UserRateLimitPolicy
    {
        public string Name { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}