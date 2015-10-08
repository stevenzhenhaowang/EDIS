namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Security
    {
        [Key]
        public string SecuritiesID { get; set; }

        [Required]
        [StringLength(128)]
        public string MarginLoanID { get; set; }

        [Column(TypeName = "date")]
        public DateTime PurchaseDate { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Ticker { get; set; }

        public int Units { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Brokerage { get; set; }

        public decimal MarketValue { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual MarginLoan MarginLoan { get; set; }
    }
}
