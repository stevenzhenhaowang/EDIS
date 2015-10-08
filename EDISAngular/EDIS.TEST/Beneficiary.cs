namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Beneficiary
    {
        public string BeneficiaryID { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Relationship { get; set; }

        [StringLength(200)]
        public string Address1 { get; set; }

        [StringLength(200)]
        public string Address2 { get; set; }

        [StringLength(200)]
        public string Address3 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public double BeneficiaryPercentage { get; set; }

        [StringLength(128)]
        public string PolicyID { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateModified { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
