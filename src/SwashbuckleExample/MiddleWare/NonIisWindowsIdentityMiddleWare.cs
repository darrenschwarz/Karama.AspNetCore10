using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SwashbuckleExample.MiddleWare
{
    /// <summary>
    /// Sets the HTTPContext.User to the WindowsIdentity
    /// </summary>    
    /// <returns></returns>
    public class NonIisWindowsIdentityMiddleWare
    {
        private readonly RequestDelegate _next;

        public NonIisWindowsIdentityMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var wi = WindowsIdentity.GetCurrent();
            var cp = new ClaimsPrincipal(wi);
            context.User = cp;
            await _next.Invoke(context);

        }
    }
}