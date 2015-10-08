namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GlobalCategoryGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GlobalCategoryGroup()
        {
            FundStocks = new HashSet<FundStock>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GlobalCategoryGroupID { get; set; }

        [Column("GlobalCategoryGroup")]
        [Required]
        [StringLength(50)]
        public string GlobalCategoryGroup1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FundStock> FundStocks { get; set; }
    }
}
