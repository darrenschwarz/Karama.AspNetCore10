using System.ComponentModel.DataAnnotations;

namespace SwashbuckleExample.ClientModels
{
    public class NewPersonCarModel
    {
        [Required]
        public int PersonId{ get; set; }
        [Required]
        public int CarId { get; set; }

    }
}