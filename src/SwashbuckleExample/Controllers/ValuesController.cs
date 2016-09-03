using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleExample.Controllers
{
    //TODO: Uncomment this and the tests will fail, saying there is no auth handler, need to set one up so we can test the full stack    
    //[Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Gets Values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets a value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Add a value
        /// </summary>
        /// <remarks>
        /// Note that there is nothing particularly noteworthy here.
        ///  
        ///     POST /Values
        ///     {
        ///        "Value": "A Value"        
        ///     }
        /// 
        /// </remarks>
        /// <param name="aValue"></param>
        [HttpPost]
        public void Post([FromBody]string aValue)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }

        /// <summary>
        /// Updates a value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]AValue value)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }

        /// <summary>
        /// Deletes a value
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }
    }

    /// <summary>
    /// An Object with a value 
    /// </summary>
    public class AValue
    {
        public string Value { get; set; }
    }
  
}
