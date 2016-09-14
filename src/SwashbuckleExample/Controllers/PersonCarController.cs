using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.ClientModels;
using System.Threading.Tasks;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.Controllers
{
    [Authorize(Policy = "IOAdmin")]
    [Route("api/[controller]")]
    public class PersonCarController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ICarRepository _carRepository;

        public PersonCarController(IPeopleRepository peopleRepository, ICarRepository carRepository)
        {
            _peopleRepository = peopleRepository;
            _carRepository = carRepository;
        }

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="personModel"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewPersonCarModel personCarModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _peopleRepository.GetAsync(personCarModel.PersonId);

            if (person == null)
            {
                return NotFound(personCarModel.PersonId);
            }

            var car = await _carRepository.GetByIdAsync(personCarModel.CarId);

            if (car == null)
            {
                return NotFound(personCarModel.CarId);
            }

            person.AddCar(new PersonCar() {Car = car, Person = person});


            await _peopleRepository.UpdateAsync(person);

            return Ok();
            // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        }
    }
}