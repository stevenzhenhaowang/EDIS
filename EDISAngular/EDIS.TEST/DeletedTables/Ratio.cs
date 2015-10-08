namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ratio
    {
        public string RatioID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        [Column(TypeName = "date")]
        public DateTime EntryDate { get; set; }

        public int RatioTypeID { get; set; }

        public double Value { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual RatioType RatioType { get; set; }
    }
}
