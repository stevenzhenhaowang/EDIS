namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ManagedFundsCurrentScore
    {
        public string ManagedFundsCurrentScoreID { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(128)]
        public string StockID { get; set; }

        public bool IsSetting { get; set; }

        public decimal OneYearReturn { get; set; }

        [Required]
        [StringLength(50)]
        public string MorningstarRecommendation { get; set; }

        public decimal OneYearAlpha { get; set; }

        public decimal OneYearBeta { get; set; }

        public decimal OneYearInfoRatio { get; set; }

        public decimal OneYearTrackError { get; set; }

        public decimal OneYearSharpRatio { get; set; }

        public decimal MaxManagementExpenseRatio { get; set; }

        public decimal PerformanceFee { get; set; }

        public decimal YearsSinceInception { get; set; }

        public int? ScoreRanking { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
