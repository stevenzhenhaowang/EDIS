namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IPOs
    {
        [Key]
        public string IPOID { get; set; }

        [StringLength(100)]
        public string IssuerName { get; set; }

        [StringLength(100)]
        public string InstrumentCode { get; set; }

        public int? OfferSize { get; set; }

        public decimal? PricePerUnit { get; set; }

        public int? Increments { get; set; }

        public int? InvestmentMin { get; set; }

        public int? InvestmentMax { get; set; }

        public decimal? ForecastRatio { get; set; }

        public int? DistributionFreq { get; set; }

        public decimal? ForecastDistribution { get; set; }

        public decimal? ForecastDividend { get; set; }

        public decimal? ForecastFranking { get; set; }

        [StringLength(50)]
        public string OfferType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LodgementDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BookbuildDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OpeningDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GeneralClosingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BrokerClosingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SettlementTradeDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HoldingDespatchDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NormalTradeDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RecordFirstPayDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstInterestDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstRedemptionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MaturityDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
