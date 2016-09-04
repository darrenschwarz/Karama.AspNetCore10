using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SwashbuckleExample;

namespace AspNetCoreRateLimit.Tests
{
    public class RateLimitFixtureBase<TStartup> : IDisposable
        where TStartup : class
    {
        private readonly TestServer _server;

        public RateLimitFixtureBase(string baseUri)
        {

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
