namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MarginLoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarginLoan()
        {
            Securities = new HashSet<Security>();
        }

        public string MarginLoanID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string HIN { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanProvider { get; set; }

        [Required]
        [StringLength(50)]
        public string VariableRateLoan { get; set; }

        public decimal VariableRateAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string FixedRateLoan { get; set; }

        public decimal FixedRateAmount { get; set; }

        public decimal UnsettledTransactions { get; set; }

        public decimal TotalBalance { get; set; }

        public decimal NetInterestYTD { get; set; }

        public decimal MonthlyInterest { get; set; }

        public decimal MinBrokerage { get; set; }

        public decimal MaxBrokerage { get; set; }

        public decimal TotalBrokerage { get; set; }

        public decimal TotalGST { get; set; }

        public decimal ManagementFee { get; set; }

        public decimal AdvisersComission { get; set; }

        public int AssetTypeID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public DateTime DateCreated { get; set; }

        public decimal LoanLimit { get; set; }

        public decimal SpendingLimit { get; set; }

        public decimal AvailableCash { get; set; }

        public DateTime DateModified { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security> Securities { get; set; }
    }
}
