using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SwashbuckleExample.AuthorizationRequirements
{
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