namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdviserCapabilityLink
    {
        public int AdviserCapabilityID { get; set; }

        [Required]
        [StringLength(128)]
        public string AdviserID { get; set; }

        public int CapabilityTypeID { get; set; }

        public bool Capable { get; set; }

        public int id { get; set; }

        public virtual CapabilityType CapabilityType { get; set; }
    }
}
