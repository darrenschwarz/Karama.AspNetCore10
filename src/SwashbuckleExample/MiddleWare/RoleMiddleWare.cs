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
            //var wi = (WindowsIdentity)context.User.Identity;
            var wi = WindowsIdentity.GetCurrent();
            wi.AddClaim(new Claim(ClaimTypes.GroupSid, "IOAdmin"));//TODO: Retieve from AD/ADAM and place in cache with an expiry, wich can also be forcibly expired,
            wi.AddClaim(new Claim(ClaimTypes.GroupSid, "SomeOther"));
            //var cp = new ClaimsPrincipal(context.User.Identity);          
            var cp = new ClaimsPrincipal(wi);
            context.User = cp;

            await _next.Invoke(context);

        }
    }
} 