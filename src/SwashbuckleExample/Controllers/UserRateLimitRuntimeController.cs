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
        /// Update CTHULHU\darren 1m UserLimitRule
        /// </summary>
        /// <param name="user"></param>
        /// <param name="limit"></param>
        [HttpPost("{user}")]
        public void Post(string user, [FromBody]int limit)//TODO: Review,currently this simply removes existing rules and add one new one.
        {
            var pol = _userPolicyStore.Get($"{_options.UserPolicyPrefix}_{user}");
            var whitelist = _options.UserWhitelist;
            pol.Rules = new List<RateLimitRule>();//TODO Allow for updating all or individual opions e.g. WhiteList

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