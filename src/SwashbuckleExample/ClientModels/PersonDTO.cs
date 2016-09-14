using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwashbuckleExample.ClientModels
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public List<CarDTO> Cars { get; set; }
    }
}
