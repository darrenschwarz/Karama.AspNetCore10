using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareApplication.EfCore.Api.Models;

namespace SoftwareApplication.EfCore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SoftwareApplicationsContext _softwareApplicationsContext;

        public ValuesController(SoftwareApplicationsContext softwareApplicationsContext)
        {
            _softwareApplicationsContext = softwareApplicationsContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var p = _softwareApplicationsContext.Packages.ToList();
            return new string[] { "value1", "value2" };
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
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
