using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreRateLimit.Tests
{
    public class ClientRateLimitTests : IClassFixture<RateLimitFixture<SwashbuckleExample.Startup>>
    {
        private const string apiPath = "/api/clients";
        private const string apiRateLimitPath = "/api/ClientRateLimit";
        private const string ip = "::1";

        public ClientRateLimitTests(RateLimitFixture<SwashbuckleExample.Startup> fixture)
        {
            Client = fixture.Client;            
        }

        public HttpClient Client { get; }


        [Fact]
        public async Task OverrideGeneralRule()
        {
            // Arrange
            var clientId = "cl-key-2";
            int responseStatusCode = 0;


            // Act    
            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, apiPath);
                request.Headers.Add("X-ClientId", clientId);
                request.Headers.Add("X-Real-IP", ip);

                var response = await Client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.NotEqual(429, responseStatusCode);

        }

        [Fact]
        public async Task WhitelistPath()
        {
            // Arrange
            var clientId = "cl-key-x";
            int responseStatusCode = 0;

            // Act    
            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, apiPath);
                request.Headers.Add("X-ClientId", clientId);
                request.Headers.Add("X-Real-IP", ip);

                var response = await Client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.NotEqual(429, responseStatusCode);
        }

        [Fact]
        public async Task WhitelistClient()
        {
            // Arrange
            var clientId = "cl-key-b";
            int responseStatusCode = 0;

            // Act    
            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiPath);
                request.Headers.Add("X-ClientId", clientId);
                request.Headers.Add("X-Real-IP", ip);

                var response = await Client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.Equal(200, responseStatusCode);
        }
    }
}
