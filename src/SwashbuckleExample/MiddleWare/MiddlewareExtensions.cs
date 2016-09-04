using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;

namespace SwashbuckleExample.MiddleWare
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextWindowsIdentityMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextWindowsIdentityMiddleWare>();
        }
    }
}
