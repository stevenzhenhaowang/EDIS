namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CapabilityGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapabilityGroup()
        {
            CapabilityTypes = new HashSet<CapabilityType>();
        }

        public int CapabilityGroupID { get; set; }

        [Column("CapabilityGroup")]
        [Required]
        [StringLength(100)]
        public string CapabilityGroup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapabilityType> CapabilityTypes { get; set; }
    }
}
