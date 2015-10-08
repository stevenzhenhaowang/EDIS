namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Loan
    {
        public string LoanID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string LoanType { get; set; }

        [Required]
        [StringLength(50)]
        public string DataFeed { get; set; }

        public decimal InterestRate { get; set; }

        public int PaymentFrequency { get; set; }

        public decimal PaymentAmount { get; set; }

        public int LoanTerm { get; set; }

        public int CreditLimit { get; set; }

        public decimal AccountBalance { get; set; }

        [Column(TypeName = "date")]
        public DateTime LoanStart { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNo { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public int AssetTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string LoanProvider { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastPaymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string FinancialInstitution { get; set; }

        [Required]
        [StringLength(100)]
        public string RelationshipManager { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string Postcode { get; set; }

        [Required]
        [StringLength(200)]
        public string RealEstateAddress { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual Client Client { get; set; }
    }
}
