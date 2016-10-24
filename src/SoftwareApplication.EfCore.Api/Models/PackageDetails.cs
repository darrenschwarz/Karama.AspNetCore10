using System;
using System.Collections.Generic;

namespace SoftwareApplication.EfCore.Api.Models
{
    public partial class PackageDetails
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string LicenceType { get; set; }

        public virtual Packages Package { get; set; }
    }
}
