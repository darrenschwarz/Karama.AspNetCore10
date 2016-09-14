using System.Collections.Generic;
using System.Threading.Tasks;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.core.interfaces
{
    /// <summary>
    /// Provides access to people
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// returns all people asynchronously
        /// </summary>
        /// <returns>Taskt&lt;List&lt;Person&gt;&gt;</returns>
        Task<List<Person>> GetAsync();
        Task<Person> GetAsync(int personId);

        Task<Person> GetByNameAsync(string name);

        Task AddAsync(Person person);

        Task UpdateAsync(Person person);
    }
}