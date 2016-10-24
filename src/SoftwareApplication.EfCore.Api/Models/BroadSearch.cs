using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareApplication.EfCore.Api.Models
{
    public class BroadSearch
    {
        [Key]
        public string PackageIdentifier { get; set; }
        public int PackageId { get; set; }
        public string Manufacturer { get; set; }
        public string LicenceType { get; set; }        
        public string Version { get; set; }
        public string GsrRef { get; set; }
    }
}
