using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SwashbuckleExample.db
{
    public class AppplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        //protected override void Seed(ApplicationDbContext context)
        //{
        //    var amodels = new List<AModel>
        //    {
        //        new AModel {Id = 1, Name="One"},
        //        new AModel {Id = 2, Name="Two"}
        //    };
        //    context.AModels.AddRange(amodels);
        //    context.SaveChanges();
        //}
    }
}
