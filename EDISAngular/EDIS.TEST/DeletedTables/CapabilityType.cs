namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CapabilityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapabilityType()
        {
            AdviserCapabilityLinks = new HashSet<AdviserCapabilityLink>();
        }

        public int CapabilityTypeID { get; set; }

        public int CapabilityGroupID { get; set; }

        [Column("CapabilityType")]
        [Required]
        [StringLength(100)]
        public string CapabilityType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdviserCapabilityLink> AdviserCapabilityLinks { get; set; }

        public virtual CapabilityGroup CapabilityGroup { get; set; }
    }
}
