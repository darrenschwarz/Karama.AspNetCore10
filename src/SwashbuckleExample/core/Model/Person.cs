using System.Collections.Generic;

namespace SwashbuckleExample.core.Model
{
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.PersonCars = new HashSet<PersonCar>();
        }

        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCar> PersonCars { get; set; }
    }
}
