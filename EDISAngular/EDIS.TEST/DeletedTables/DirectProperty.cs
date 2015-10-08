namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DirectProperty
    {
        public string DirectPropertyID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public decimal PurchasedPrice { get; set; }

        public decimal StampDuty { get; set; }

        public decimal PurchaseMiscFee { get; set; }

        public decimal PurchaseLegalFee { get; set; }

        public decimal PurchaseBankFee { get; set; }

        [Column(TypeName = "date")]
        public DateTime PurchasedDate { get; set; }

        public decimal? SalePrice { get; set; }

        public decimal? AgentCommission { get; set; }

        public decimal? SaleMiscFee { get; set; }

        public decimal? SaleLegalFee { get; set; }

        public decimal? SaleBankFee { get; set; }

        public decimal? SaleLoanRepayment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SaleDate { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public decimal EstimatedValue { get; set; }

        public decimal RentalIncome { get; set; }

        public int OwnershipLength { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public decimal? OutstandingLoan { get; set; }

        public decimal? YearsOnLoan { get; set; }

        [StringLength(50)]
        public string LoanRateType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FixedTermEndDate { get; set; }

        public decimal? FixedRate { get; set; }

        public decimal? FixedLoanPercent { get; set; }

        public decimal? InterestRepayment { get; set; }

        public decimal? VariableRate { get; set; }

        [StringLength(100)]
        public string AgentName { get; set; }

        [StringLength(100)]
        public string AgentCompany { get; set; }

        [StringLength(200)]
        public string AgentAddress { get; set; }

        [StringLength(50)]
        public string AgentPhone { get; set; }

        [StringLength(50)]
        public string AgentFax { get; set; }

        [StringLength(100)]
        public string AgentEmail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentYearFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentYearTo { get; set; }

        public int AssetTypeID { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }
    }
}
