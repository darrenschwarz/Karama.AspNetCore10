using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SwashbuckleExample.AuthorizationRequirements
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>//TODO: Refactor this is just for spike
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if(context.User != null)
            {
                if (context.User.IsInRole(requirement.Role))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.FromResult(0);
        }
    }
}