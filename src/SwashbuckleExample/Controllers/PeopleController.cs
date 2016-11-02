using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.core.Model;
using SwashbuckleExample.ClientModels;
using SwashbuckleExample.db;

namespace SwashbuckleExample.Controllers
{
   // [Authorize]
    [Authorize(Policy = "IOAdmin")]
    //[Authorize(Roles = "IOAdmin")]
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Gets Values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var peopleList = await _peopleRepository.GetAsync();

                var result = peopleList.Select(person => new PersonDTO()
                {
                    Id = person.Id,
                    Age = person.Age,
                    Name = person.Name,
                    Cars = person.PersonCars.Select(car => new CarDTO() { Id = car.Id, Make = car.Car.Make }).ToList()
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Gets a value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _peopleRepository.GetAsync(id);

            if (person == null)
            {
                return NotFound(id);
            }

            var result = new PersonDTO()
            {
                Id = person.Id,
                Age = person.Age,
                Name = person.Name,
                Cars = person.PersonCars.Select(car => new CarDTO() {Id = car.Id, Make = car.Car.Make}).ToList()
            };
            return Ok(result);
;        }

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="personModel"></param>
        [HttpPost]
        [Produces(typeof(Person))]
        public async Task<IActionResult> Post([FromBody]NewPersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _peopleRepository.GetByNameAsync(personModel.Name);

            if (person != null)
            {
                return StatusCode(409, $"{personModel.Name} already exists."); //BadRequest($"{personModel.Name} already exists.");
            }

           var personResult =  await _peopleRepository.AddAsync(new Person()
            {
                Age = personModel.Age,
                Name = personModel.Name
            });

            return CreatedAtAction("Get", new {id = personResult.Id}, personResult);
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }

        /// <summary>
        /// Updates a value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
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
}
