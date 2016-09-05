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
            //var identity = new GenericIdentity("CTHULHU\\fred");
            //var principal = new GenericPrincipal(identity, null);
            //Thread.CurrentPrincipal = principal;
            //var cp = new ClaimsPrincipal(Thread.CurrentPrincipal);

            var cp = new ClaimsPrincipal(WindowsIdentity.GetCurrent());


            context.User = cp;
            await _next.Invoke(context);

        }

    }
}