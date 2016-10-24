using System;
using System.Collections.Generic;

namespace SoftwareApplication.EfCore.Api.Models
{
    public partial class SoftwareAutomation
    {
        public SoftwareAutomation()
        {
            Gsr = new HashSet<Gsr>();
        }

        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Version { get; set; }
        public string Manufacturer { get; set; }

        public virtual ICollection<Gsr> Gsr { get; set; }
        public virtual Packages Package { get; set; }
    }
}
