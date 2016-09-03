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
}
