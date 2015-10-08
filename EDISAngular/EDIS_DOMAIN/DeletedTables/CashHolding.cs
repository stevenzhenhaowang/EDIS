namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CashHolding
    {
        [Key]
        public string CashHoldingsID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public int AssetTypeID { get; set; }

        public decimal? InterestRate { get; set; }

        public decimal? Frequency { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextPayment { get; set; }

        public decimal? DebitInterest { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOpened { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [StringLength(50)]
        public string StatementMethod { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }
    }
}
