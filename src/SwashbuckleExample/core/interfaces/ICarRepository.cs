using System.Threading.Tasks;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.core.interfaces
{
    public interface ICarRepository
    {
        Task<Car> GetByIdAsync(int Id);
    }
}