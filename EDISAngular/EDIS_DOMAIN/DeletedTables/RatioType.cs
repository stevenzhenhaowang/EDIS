namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RatioType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RatioType()
        {
            Ratios = new HashSet<Ratio>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RatioTypeID { get; set; }

        [Required]
        [StringLength(200)]
        public string RatioTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ratio> Ratios { get; set; }
    }
}
