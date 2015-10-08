namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FixedIncomeHolding
    {
        [Key]
        public string FixedIncomeID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [StringLength(100)]
        public string IssuerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Ticker { get; set; }

        public decimal? InterestPaid { get; set; }

        public decimal? MaturityValue { get; set; }

        public int FixedTerm { get; set; }

        public decimal? BondPrice { get; set; }

        public decimal? TransactionFee { get; set; }

        public decimal? MinimumFee { get; set; }

        public decimal? MaximumFee { get; set; }

        [StringLength(50)]
        public string AccountNo { get; set; }

        [StringLength(50)]
        public string AccountUserID { get; set; }

        [StringLength(50)]
        public string ClearingSponsor { get; set; }

        [StringLength(50)]
        public string HIN { get; set; }

        public int AssetTypeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextPaymentDate { get; set; }

        public decimal? NextPaymentAmount { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }
    }
}
