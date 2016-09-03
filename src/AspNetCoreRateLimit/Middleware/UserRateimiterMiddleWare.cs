using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetCoreRateLimit
{
    public class UserRateimiterMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserRateimiterMiddleWare> _logger;

        public UserRateimiterMiddleWare(RequestDelegate next, ILogger<UserRateimiterMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var user = httpContext.User;

            await _next.Invoke(httpContext);
        }
    }
}