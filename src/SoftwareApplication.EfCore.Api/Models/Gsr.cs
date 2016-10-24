using System;
using System.Collections.Generic;

namespace SoftwareApplication.EfCore.Api.Models
{
    public partial class Gsr
    {
        public int Id { get; set; }
        public string GsrRef { get; set; }
        public int SotwareAutomationId { get; set; }

        public virtual SoftwareAutomation SotwareAutomation { get; set; }
    }
}
