using System.Collections.Generic;
using System.Data.Entity;
using SwashbuckleExample.core.interfaces;
using SwashbuckleExample.core.Model;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SwashbuckleExample.db
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PeopleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Person>> GetAsync()
        {
            return _dbContext.People.ToListAsync();
        }

        public Task<Person> GetAsync(int personId)
        {
            return _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == personId);
        }

        public Task<Person> GetByNameAsync(string name)
        {
            return _dbContext.People
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Person> AddAsync(Person person)
        {
            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public Task UpdateAsync(Person person)
        {
            _dbContext.Entry(person).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}