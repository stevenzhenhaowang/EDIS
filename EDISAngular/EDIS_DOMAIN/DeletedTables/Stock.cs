namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            AustEquityCurrentScores = new HashSet<AustEquityCurrentScore>();
            AustEquityHistoricalScores = new HashSet<AustEquityHistoricalScore>();
            DailyPrices = new HashSet<DailyPrice>();
            DividendHistories = new HashSet<DividendHistory>();
            InterEquityCurrentScores = new HashSet<InterEquityCurrentScore>();
            InterEquityHistoricalScores = new HashSet<InterEquityHistoricalScore>();
            ManagedFundsCurrentScores = new HashSet<ManagedFundsCurrentScore>();
            ManagedFundsHistoricalScores = new HashSet<ManagedFundsHistoricalScore>();
            MorningStarRatings = new HashSet<MorningStarRating>();
            Ratios = new HashSet<Ratio>();
            StockDistributions = new HashSet<StockDistribution>();
            StockHoldings = new HashSet<StockHolding>();
            StockTransactions = new HashSet<StockTransaction>();
        }

        public string StockID { get; set; }

        [Required]
        [StringLength(128)]
        public string FundStockID { get; set; }

        public string Ticker { get; set; }

        [Required]
        [StringLength(10)]
        public string APIRCode { get; set; }

        [Required]
        [StringLength(50)]
        public string SecID { get; set; }

        [Required]
        [StringLength(20)]
        public string ISIN { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string FirmName { get; set; }

        [StringLength(100)]
        public string BrandingName { get; set; }

        [StringLength(50)]
        public string ManagerName { get; set; }

        public double? ManagerTenureAVG { get; set; }

        public double? ManagerTenureLONG { get; set; }

        public int ExchangeID { get; set; }

        [Required]
        [StringLength(50)]
        public string BusinessCountry { get; set; }

        [Required]
        [StringLength(50)]
        public string Domicile { get; set; }

        public int BaseCurrencyID { get; set; }

        [Required]
        [StringLength(10)]
        public string SecurityType { get; set; }

        public int InvestmentTypeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime InceptionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime IPODate { get; set; }

        public double DateMarketCap { get; set; }

        public double DividedDistributionFrequency { get; set; }

        public decimal DividedYTD { get; set; }

        public DateTime DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AustEquityCurrentScore> AustEquityCurrentScores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AustEquityHistoricalScore> AustEquityHistoricalScores { get; set; }

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyPrice> DailyPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DividendHistory> DividendHistories { get; set; }

        public virtual Exchanx Exchanx { get; set; }

        public virtual FundStock FundStock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterEquityCurrentScore> InterEquityCurrentScores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterEquityHistoricalScore> InterEquityHistoricalScores { get; set; }

        public virtual InvestmentType InvestmentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagedFundsCurrentScore> ManagedFundsCurrentScores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagedFundsHistoricalScore> ManagedFundsHistoricalScores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MorningStarRating> MorningStarRatings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ratio> Ratios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDistribution> StockDistributions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockHolding> StockHoldings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
