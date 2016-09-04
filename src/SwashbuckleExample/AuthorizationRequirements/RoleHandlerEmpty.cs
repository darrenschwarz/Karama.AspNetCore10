using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SwashbuckleExample.AuthorizationRequirements
{
    public class RoleHandlerEmpty : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            //Implement AdamService here:
            if (requirement.Role.Contains("IOAdmin"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}