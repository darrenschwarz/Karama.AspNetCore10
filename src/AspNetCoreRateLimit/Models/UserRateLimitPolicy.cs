using System.Collections.Generic;

namespace AspNetCoreRateLimit
{
    public class UserRateLimitPolicy
    {
        public string User { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}