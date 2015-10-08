namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Referral
    {
        public string ReferralID { get; set; }

        [StringLength(50)]
        public string TempEDISAccountNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Individual { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [StringLength(20)]
        public string WorkPhone { get; set; }

        [StringLength(20)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string ExistingArrangement { get; set; }

        public bool PaymentMade { get; set; }

        public decimal? PaymentAmount { get; set; }

        [StringLength(50)]
        public string ReferralPurpose { get; set; }

        [Required]
        [StringLength(50)]
        public string SpecialInstructions { get; set; }

        [Required]
        [StringLength(128)]
        public string AdviserID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
