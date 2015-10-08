namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Policy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Policy()
        {
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public string PolicyID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        [Required]
        [StringLength(100)]
        public string InsuranceType { get; set; }

        [Required]
        [StringLength(100)]
        public string PolicyType { get; set; }

        [Required]
        [StringLength(100)]
        public string PolicyName { get; set; }

        [StringLength(50)]
        public string PolicyNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountName { get; set; }

        [StringLength(200)]
        public string AccountAddress1 { get; set; }

        [StringLength(200)]
        public string AccountAddress2 { get; set; }

        [StringLength(200)]
        public string AccountAddress3 { get; set; }

        [StringLength(100)]
        public string AccountCity { get; set; }

        [StringLength(100)]
        public string AccountState { get; set; }

        [StringLength(100)]
        public string AccountPostCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InceptionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastRenewalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateModified { get; set; }

        public string Commentary { get; set; }

        [StringLength(100)]
        public string Institution { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RenewalDueDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }

        public virtual Client Client { get; set; }

        public virtual PolicyDetail PolicyDetail { get; set; }
    }
}
