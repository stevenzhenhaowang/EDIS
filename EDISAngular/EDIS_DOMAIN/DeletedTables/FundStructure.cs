namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FundStructure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FundStructure()
        {
            FundStocks = new HashSet<FundStock>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FundStructureID { get; set; }

        [Column("FundStructure")]
        [Required]
        [StringLength(200)]
        public string FundStructure1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FundStock> FundStocks { get; set; }
    }
}
