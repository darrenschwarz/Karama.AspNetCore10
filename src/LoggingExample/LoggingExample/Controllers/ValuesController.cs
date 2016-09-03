using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("This happened");
            _logger.LogError("This Error happened");
            var userName = User.Identity.Name;
            return new string[] { "value1", "value2", userName };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }
    }
}
