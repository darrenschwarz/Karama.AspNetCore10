using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetCoreRateLimit.Tests
{
    public class RateLimitFixtureBase<TStartup> : IDisposable
        where TStartup : class
    {
        private readonly TestServer _server;

        public RateLimitFixtureBase(string baseUri)
        {
            //var wi = (System.Security.Principal.WindowsIdentity)Thread.CurrentPrincipal;

            //var wic = wi.Impersonate();

            var builder = new WebHostBuilder().UseStartup<TStartup>();            
            _server = new TestServer(builder);

            

            //var s = _server.Host.Services.GetService(typeof(ClaimsIdentity));

            //_server.Host.ServerFeatures.Set<ClaimsPrincipal>(ClaimsPrincipal.Current);


            Client = _server.CreateClient();
            
            Client.BaseAddress = new Uri(baseUri);
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
