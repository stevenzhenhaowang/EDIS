namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockTransaction()
        {
            StockHoldings = new HashSet<StockHolding>();
        }

        public string StockTransactionID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        [Required]
        [StringLength(128)]
        public string AdviserID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ContractNo { get; set; }

        public int Units { get; set; }

        public decimal Price { get; set; }

        public decimal BrokerageFee { get; set; }

        public int TransactionTypeID { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockHolding> StockHoldings { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual TransactionType TransactionType { get; set; }
    }
}
