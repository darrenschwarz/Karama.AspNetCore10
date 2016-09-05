using System.Collections.Generic;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SwashbuckleExample.Controllers
{
    [Route("api/[controller]")]
    public class UserRateLimitRuntimeController : Controller
    {
        private readonly UserRateLimitOptions _options;
        private readonly IUserPolicyStore _userPolicyStore;

        public UserRateLimitRuntimeController(IOptions<UserRateLimitOptions> optionsAccessor, IUserPolicyStore userPolicyStore)
        {
            _options = optionsAccessor.Value;
            _userPolicyStore = userPolicyStore;
        }

        /// <summary>
        /// Update CHULHU\darren 1m UserLimitRule
        /// </summary>
        /// <param name="limit"></param>
        [HttpPost]
        public void Post([FromBody]int limit)
        {
            var pol = _userPolicyStore.Get(_options.UserPolicyPrefix);

            pol.Rules = new List<RateLimitRule>();

            pol.Rules.Add(new RateLimitRule
            {
                Endpoint = "get:/api/values",
                Limit = limit,
                Period = "1m"                 
            });

            _userPolicyStore.Set(_options.UserPolicyPrefix, pol);
        }
    }
}