namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DailyPrice
    {
        public string DailyPriceID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        [Column(TypeName = "date")]
        public DateTime High52WeekDate { get; set; }

        public decimal High52WeekPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime Low52WeekDate { get; set; }

        public decimal Low52WeekPrice { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
