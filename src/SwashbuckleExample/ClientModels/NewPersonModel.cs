using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SwashbuckleExample.ClientModels
{
    public class NewPersonModel
    {
        [Required]
        [Range(2, 10)]
        public int Age { get; set; }

        [Required]     
        [MinLength(3)] 
        [MaxLength(10)]  
        public string Name { get; set; }
    }
}
