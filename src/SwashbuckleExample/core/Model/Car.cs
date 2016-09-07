using System.Collections.Generic;

namespace SwashbuckleExample.core.Model
{
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.PersonCars = new HashSet<PersonCar>();
        }

        public int Id { get; set; }
        public string Make { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCar> PersonCars { get; set; }
    }
}