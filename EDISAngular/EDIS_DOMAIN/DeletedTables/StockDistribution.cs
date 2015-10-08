namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockDistribution
    {
        public string StockDistributionID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockHoldingID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime DistributionDate { get; set; }

        [StringLength(50)]
        public string DistributionType { get; set; }

        public decimal Amount { get; set; }

        public decimal Franking { get; set; }

        public int Units { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExDates { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        public virtual Client Client { get; set; }

        public virtual StockHolding StockHolding { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
