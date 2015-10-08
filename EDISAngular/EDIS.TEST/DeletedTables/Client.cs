namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Appointments = new HashSet<Appointment>();
            CashHoldings = new HashSet<CashHolding>();
            ComplianceIncomeExpenses = new HashSet<ComplianceIncomeExpens>();
            Dependants = new HashSet<Dependant>();
            FixedIncomeHoldings = new HashSet<FixedIncomeHolding>();
            Loans = new HashSet<Loan>();
            MarginLoans = new HashSet<MarginLoan>();
            Notes = new HashSet<Note>();
            Policies = new HashSet<Policy>();
            DirectProperties = new HashSet<DirectProperty>();
            RiskProfiles = new HashSet<RiskProfile>();
            StockDistributions = new HashSet<StockDistribution>();
            StockHoldings = new HashSet<StockHolding>();
            StockTransactions = new HashSet<StockTransaction>();
        }

        public string ClientID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(128)]
        public string ClientGroupID { get; set; }

        [StringLength(100)]
        public string DesignationAccount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastContact { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastReview { get; set; }

        public int? RiskProfileOutcome { get; set; }

        public byte[] Image { get; set; }

        [StringLength(256)]
        public string ImageMimeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashHolding> CashHoldings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComplianceIncomeExpens> ComplianceIncomeExpenses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dependant> Dependants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixedIncomeHolding> FixedIncomeHoldings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarginLoan> MarginLoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policy> Policies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DirectProperty> DirectProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RiskProfile> RiskProfiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDistribution> StockDistributions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockHolding> StockHoldings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
