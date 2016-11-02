using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreRateLimit.Tests
{
    public class UserRateLimitTests : IClassFixture<RateLimitFixture<SwashbuckleExample.Startup>>
    {
        private const string apiPeoplePath = "/api/people";

        private const string ip = "::1";

        public UserRateLimitTests(RateLimitFixture<SwashbuckleExample.Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Theory]
        [InlineData("GET")]        
        public async Task SpecificUserRuleExceedsThreeCallsPerMinute(string verb)
        {
            // Arrange
            int responseStatusCode = 0;
            var clientId = "cl-key-1";

            // Act    
            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(new HttpMethod(verb), apiPeoplePath);
                request.Headers.Add("X-Real-IP", ip);
                request.Headers.Add("X-ClientId", clientId);                
                
                var response = await Client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.Equal(429, responseStatusCode);
        }


        [Theory]
        [InlineData("GET")]
        public async Task SpecificUserRuleWithinThreeCallsPerMinute(string verb)
        {
            // Arrange
            int responseStatusCode = 0;
            var clientId = "cl-key-1";

            // Act    
            for (int i = 0; i < 2; i++)
            {
                var request = new HttpRequestMessage(new HttpMethod(verb), apiPeoplePath);
                request.Headers.Add("X-Real-IP", ip);
                request.Headers.Add("X-ClientId", clientId);

                var response = await Client.SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
                Thread.Sleep(1000);
            }

            // Assert
            Assert.Equal(200, responseStatusCode);
        }        
    }
}