namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockHolding
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockHolding()
        {
            StockDistributions = new HashSet<StockDistribution>();
        }

        public string StockHoldingID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int Units { get; set; }

        [Required]
        [StringLength(128)]
        public string LastTransactionID { get; set; }

        public decimal? ServiceFee { get; set; }

        public int AssetTypeID { get; set; }

        [StringLength(50)]
        public string HIN { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDistribution> StockDistributions { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual StockTransaction StockTransaction { get; set; }
    }
}
