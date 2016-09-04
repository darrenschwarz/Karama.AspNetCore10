using Microsoft.AspNetCore.Authorization;

namespace SwashbuckleExample.AuthorizationRequirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }
}
