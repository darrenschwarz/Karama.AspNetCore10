using System.Threading.Tasks;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.core.interfaces
{
    public interface IPersonCarRepository
    {
        Task AddAsync(PersonCar personCar);
    }
}