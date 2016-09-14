using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.core.Model;
using SwashbuckleExample.ClientModels;
using SwashbuckleExample.Controllers;
using Xunit;

namespace SwashbuckleExample.Tests.UnitTests
{
    public class PersonCarControllerTests
    {
        public PersonCarControllerTests()
        {
        }


        [Fact]
        public async Task Post_ReturnsTaskCompletedTask()
        {
            // Arrange
            int carId = 1;
            int personId = 1;
            var testPerson = GetTestPerson();
            var mockPeopleRepo = new Mock<IPeopleRepository>();
            mockPeopleRepo.Setup(repo => repo.GetAsync(personId))
                .Returns(Task.FromResult(testPerson));

            var testCar = GetTestCar();
            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(repo => repo.GetByIdAsync(carId))
                .Returns(Task.FromResult(testCar));

            var controller = new PersonCarController(mockPeopleRepo.Object, mockCarRepo.Object);

            var newPersonCar = new NewPersonCarModel()
            {
                CarId = carId,
                PersonId = personId
            };

            mockPeopleRepo.Setup(repo => repo.UpdateAsync(testPerson))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await controller.Post(newPersonCar);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            mockPeopleRepo.Verify();
        }

        private Person GetTestPerson()
        {
            var person = new Person()
            {                
                Id = 1,
                Name = "Darren",
                Age = 42,
            };

            var personCar = new PersonCar()
            {
                Id = 1,
                Car = new Car() { Id = 1, Make = "Golf"},
                Person = person
            };
            person.AddCar(personCar);;
            return person;
        }

        private Car GetTestCar()
        {
            var car = new Car()
            {
                Id = 1,
                Make= "Golf"
            };
            return car;
        }
    }
}
