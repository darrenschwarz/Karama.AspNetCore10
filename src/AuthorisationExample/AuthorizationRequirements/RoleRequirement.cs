using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthorisationExample.AuthorizationRequirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }

    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.FromResult(0);
            }

            //Implement AdamService here:
            if (requirement.Role.Contains("IOAdmin"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
