namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ManagedFundsHistoricalScore
    {
        public string ManagedFundsHistoricalScoreID { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(128)]
        public string StockID { get; set; }

        public bool IsSetting { get; set; }

        public decimal FiveYearReturn { get; set; }

        public decimal FiveYearAlpha { get; set; }

        public decimal FiveYearBeta { get; set; }

        public decimal FiveYearInfoRatio { get; set; }

        public decimal FiveYearStdDev { get; set; }

        public decimal FiveYearSkewRatio { get; set; }

        public decimal FiveYearTrackError { get; set; }

        public decimal FiveYearSharpRatio { get; set; }

        public decimal GlobalCategory { get; set; }

        public int FundSize { get; set; }

        public int? ScoreRanking { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
