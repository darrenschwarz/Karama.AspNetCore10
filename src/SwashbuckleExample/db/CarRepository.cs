using System.Threading.Tasks;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.core.Model;
using System.Data.Entity;

namespace SwashbuckleExample.db
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Car> GetByIdAsync(int Id)
        {
            return _dbContext.Cars.FirstOrDefaultAsync(car => car.Id == Id);
        }
    }
}