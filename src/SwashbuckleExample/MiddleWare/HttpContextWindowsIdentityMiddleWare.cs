using System;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SwashbuckleExample.MiddleWare
{
    /// <summary>
    /// Sets the HTTPContext.User to the WindowsIdentity
    /// </summary>    
    /// <returns></returns>
    public class HttpContextWindowsIdentityMiddleWare
    {
        private readonly RequestDelegate _next;

        public HttpContextWindowsIdentityMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            var wi = WindowsIdentity.GetCurrent();
            wi.AddClaim(new Claim(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "IOAdmin"));
            
            var cp = new ClaimsPrincipal(wi);

            context.User = cp;
            
            await _next.Invoke(context);

        }

    }
}