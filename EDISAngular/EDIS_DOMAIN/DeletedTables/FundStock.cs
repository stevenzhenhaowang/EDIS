namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FundStock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FundStock()
        {
            Stocks = new HashSet<Stock>();
        }

        public string FundStockID { get; set; }

        public int InvestmentStrategyID { get; set; }

        public int PortfolioCurrencyID { get; set; }

        public int PricingFrequency { get; set; }

        public int NAVDailyBaseFrequency { get; set; }

        public int NAVMoEnd { get; set; }

        public int FundStructureID { get; set; }

        public int FinancialHealthGradeID { get; set; }

        public bool Wholesale { get; set; }

        public bool Institutional { get; set; }

        public int GlobalCategoryGroupID { get; set; }

        [StringLength(100)]
        public string PrimaryProspectusBenchmark { get; set; }

        public int? PrimaryProspectusBenchmarkID { get; set; }

        public int FundSizeBaseCurrencyID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FundSizeDate { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Currency Currency1 { get; set; }

        public virtual FinancialHealthGrade FinancialHealthGrade { get; set; }

        public virtual FundStructure FundStructure { get; set; }

        public virtual GlobalCategoryGroup GlobalCategoryGroup { get; set; }

        public virtual Strategy Strategy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
