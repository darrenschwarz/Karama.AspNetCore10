using System.Threading.Tasks;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.db
{
    public class PersonCarRepository : IPersonCarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonCarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(PersonCar personCar)
        {
            _dbContext.PersonCars.Add(personCar);
            return _dbContext.SaveChangesAsync();
        }
    }
}