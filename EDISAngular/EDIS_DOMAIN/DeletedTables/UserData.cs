namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserData")]
    public partial class UserData
    {
        [Key]
        public string UserID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string PreferredName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [StringLength(20)]
        public string HomePhone { get; set; }

        [StringLength(20)]
        public string WorkPhone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string WorkEmail { get; set; }

        [StringLength(100)]
        public string HomeEmail { get; set; }

        public bool? Active { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(50)]
        public string ABN { get; set; }

        [StringLength(50)]
        public string ACN { get; set; }

        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string PostalAddressLine1 { get; set; }

        [StringLength(100)]
        public string PostalAddressLine2 { get; set; }

        [StringLength(100)]
        public string PostalCity { get; set; }

        [StringLength(100)]
        public string PostalState { get; set; }

        [StringLength(10)]
        public string PostalPostcode { get; set; }

        [StringLength(100)]
        public string PostalCountry { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
