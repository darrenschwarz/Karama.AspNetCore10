using System.Collections.Generic;

namespace SwashbuckleExample.core.Model
{
    public partial class PersonCar
    {
        public int Id { get; set; }

        public virtual Car Car { get; set; }
        public virtual Person Person { get; set; }
    }
}