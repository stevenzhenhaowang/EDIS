namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InterEquityHistoricalScore
    {
        public string InterEquityHistoricalScoreID { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(128)]
        public string StockID { get; set; }

        public bool IsSetting { get; set; }

        public decimal OneYearReturn { get; set; }

        public long? MarketCapitalisation { get; set; }

        public decimal DIVYield { get; set; }

        public decimal ROA { get; set; }

        public decimal ROE { get; set; }

        public decimal QuickRatio { get; set; }

        public decimal CurrentRatio { get; set; }

        public decimal DebtEquityRatio { get; set; }

        public decimal PERatio { get; set; }

        public decimal Beta5Year { get; set; }

        public int? ScoreRanking { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
