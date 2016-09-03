using System.Collections.Generic;
using System.Linq;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SwashbuckleExample.Controllers
{
    [Route("api/[controller]")]
    public class IpRateLimitRuntimeController : Controller
    {
        private readonly IpRateLimitOptions _options;
        private readonly IIpPolicyStore _ipPolicyStore;

        public IpRateLimitRuntimeController(IOptions<IpRateLimitOptions> optionsAccessor, IIpPolicyStore ipPolicyStore)
        {
            _options = optionsAccessor.Value;
            _ipPolicyStore = ipPolicyStore;
        }

        /// <summary>
        /// Update a RateLimitRule
        /// </summary>
        /// <param name="limit"></param>
        [HttpPost]
        public void Post([FromBody]int limit)
        {
            var pol = _ipPolicyStore.Get(_options.IpPolicyPrefix);

            pol.IpRules = new List<IpRateLimitPolicy>();

            pol.IpRules.Add(new IpRateLimitPolicy
            {
                Ip = "::1/10",
                Rules = new List<RateLimitRule>(new RateLimitRule[] {
                    new RateLimitRule {
                        Endpoint = "get:/api/values",
                        Limit = limit,
                        Period = "1m" }
                })
            });

            _ipPolicyStore.Set(_options.IpPolicyPrefix, pol);
        }

        /// <summary>
        /// Update a RateLimitRule
        /// </summary>
        /// <param name="ruleReplacement"></param>
        /// <remarks>        
        /// {
        ///  "ip": "::1/10",
        ///  "rateLimitRuleExisting": {
        ///    "endpoint": "get:/api/values",
        ///    "period": "1m",
        ///    "periodTimespan": "string",
        ///    "limit": 2
        ///  },
        ///  "rateLimitRuleNew": {
        ///    "endpoint": "get:/api/values",
        ///    "period": "1m",
        ///    "periodTimespan": "string",
        ///    "limit": 4
        ///  }
        ///}
        /// </remarks>
        [HttpPut]
        public void Put([FromBody]RateLimitRuleReplacement ruleReplacement)
        {
            var pol = _ipPolicyStore.Get(_options.IpPolicyPrefix);
            var rateLimitPolicies = pol.IpRules.FirstOrDefault(r => r.Ip == ruleReplacement.Ip);
            if (rateLimitPolicies != null)
            {
                var rule = rateLimitPolicies.Rules.FirstOrDefault(
                    e => e.Endpoint == ruleReplacement.RateLimitRuleExisting.Endpoint
                         && e.Limit == ruleReplacement.RateLimitRuleExisting.Limit
                         && e.Period == ruleReplacement.RateLimitRuleExisting.Period);

                if (rule != null) rule.Limit = ruleReplacement.RateLimitRuleNew.Limit;
            }
        }

        /// <summary>
        /// Describes an existing RateLimitRule, and it's replacement
        /// </summary>
        public class RateLimitRuleReplacement
        {
            public string Ip { get; set; }
            //public AValue Val { get; set; }
            public RateLimitRule RateLimitRuleExisting { get; set; }
            public RateLimitRule RateLimitRuleNew { get; set; }
        }

    }
}