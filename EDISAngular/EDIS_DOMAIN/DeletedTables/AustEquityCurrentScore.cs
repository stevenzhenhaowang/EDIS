namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AustEquityCurrentScore
    {
        public string AustEquityCurrentScoreID { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(128)]
        public string StockID { get; set; }

        public bool IsSetting { get; set; }

        public decimal EPSGrowth { get; set; }

        [Required]
        [StringLength(50)]
        public string MorningstarRecommendation { get; set; }

        public decimal DIVYield { get; set; }

        public decimal Frank { get; set; }

        public decimal ROA { get; set; }

        public decimal ROE { get; set; }

        public decimal IntCover { get; set; }

        public decimal DebtEquity { get; set; }

        public decimal PE { get; set; }

        public decimal IntrinsicValueVariation { get; set; }

        public int? ScoreRanking { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
