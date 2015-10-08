namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Strategy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Strategy()
        {
            FundStocks = new HashSet<FundStock>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StrategyID { get; set; }

        [Column("Strategy")]
        [Required]
        [StringLength(100)]
        public string Strategy1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FundStock> FundStocks { get; set; }
    }
}
