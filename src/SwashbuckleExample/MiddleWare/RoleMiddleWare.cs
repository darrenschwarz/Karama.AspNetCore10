using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SwashbuckleExample.MiddleWare
{
    /// <summary>
    /// Add Clamis e.g. Roles(aka groupsid) to the HTTPContext.User
    /// </summary>    
    /// <returns></returns>
    public class RoleMiddleWare
    {
        private readonly RequestDelegate _next;

        public RoleMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            const string Issuer = "http://air.co.com";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, context.User.Identity.Name, ClaimValueTypes.String, Issuer),
                new Claim(ClaimTypes.Role, "IOAdmins", ClaimValueTypes.String, Issuer)
            };

            var userIdentity = new ClaimsIdentity(claims, "Windows");

            //var userIdentity = WindowsIdentity.GetCurrent();
            //userIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, "IOAdmin"));//TODO: Retieve from AD/ADAM and place in cache with an expiry, wich can also be forcibly expired,
            //userIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, "SomeOther"));      
            //var cp = new ClaimsPrincipal(userIdentity);
            var cp = new ClaimsPrincipal(userIdentity);

            context.User = cp;

            await _next.Invoke(context);

        }
    }
} 