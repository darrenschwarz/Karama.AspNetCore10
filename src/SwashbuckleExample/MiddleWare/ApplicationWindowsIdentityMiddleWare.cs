using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SwashbuckleExample.MiddleWare
{
    public class ApplicationWindowsIdentityMiddleWare
    {

        private readonly RequestDelegate _next;

        public ApplicationWindowsIdentityMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            //TODO: This is where we would impersonate the idetity of the application, akin to setting the AppPool Identity
            using (WindowsImpersonationContext impersonationContext = (WindowsIdentity.GetCurrent()).Impersonate())
            {                
                await _next.Invoke(context);
            }

        }
    }
}