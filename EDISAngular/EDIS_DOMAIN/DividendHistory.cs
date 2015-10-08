namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DividendHistory
    {
        public string DividendHistoryID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int CurrencyID { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

      
    }
}
