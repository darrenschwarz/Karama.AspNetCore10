using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SwashbuckleExample.core.Model;

namespace SwashbuckleExample.db
{
    [DbConfigurationType(typeof(CodeConfig))]
    public class ApplicationDbContext : DbContext
    {
        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new AppplicationDbInitializer());
        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
//            modelBuilder.Entity<Person>()
//        .HasRequired(c => c.PersonCars)
//        .WithRequiredDependent()
//        .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Car>()
//.HasRequired(c => c.PersonCars)
//.WithRequiredDependent()
//.WillCascadeOnDelete(false);
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonCar> PersonCars { get; set; }
    }
}
