using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SwashbuckleExample.MiddleWare
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseNonIisWindowsIdentityMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NonIisWindowsIdentityMiddleWare>();
        }

        public static IApplicationBuilder UseRoleMiddleWare(this IApplicationBuilder builder)
        {
             return builder.UseMiddleware<RoleMiddleWare>();
        }
    }
}
