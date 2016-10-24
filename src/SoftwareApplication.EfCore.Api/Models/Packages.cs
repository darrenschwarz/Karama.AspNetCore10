using System;
using System.Collections.Generic;

namespace SoftwareApplication.EfCore.Api.Models
{
    public partial class Packages
    {
        public Packages()
        {
            PackageDetails = new HashSet<PackageDetails>();
            SoftwareAutomation = new HashSet<SoftwareAutomation>();
        }

        public int Id { get; set; }
        public string PackageIdentifier { get; set; }
        public string PackageName { get; set; }
        public string PackageManufacturer { get; set; }
        public string PackageVersion { get; set; }

        public virtual ICollection<PackageDetails> PackageDetails { get; set; }
        public virtual ICollection<SoftwareAutomation> SoftwareAutomation { get; set; }
    }
}
