using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SwashbuckleExample.AuthorizationRequirements
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>//TODO: Refactor this is just for spike
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.FromResult(0);
            }

            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                if (string.Compare(requirement.Role, 0,context.User.FindFirst(ClaimTypes.Role).Value, 0, requirement.Role.Length, false) == 0)
                {
                    context.Succeed(requirement);
                }
            }
            


            return Task.FromResult(0);
        }
    }
}