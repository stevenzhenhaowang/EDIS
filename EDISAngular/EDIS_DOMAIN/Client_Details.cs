namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_Details
    {
        [Key]
        public string UserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(16)]
        public string Title { get; set; }

        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string MiddleName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(32)]
        public string Gender { get; set; }

        [StringLength(32)]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [StringLength(32)]
        public string Mobile { get; set; }

        [StringLength(128)]
        public string AddressLn1 { get; set; }

        [StringLength(128)]
        public string AddressLn2 { get; set; }

        [StringLength(128)]
        public string AddressLn3 { get; set; }

        [StringLength(64)]
        public string Suburb { get; set; }

        [StringLength(64)]
        public string State { get; set; }

        [StringLength(64)]
        public string PostCode { get; set; }

        [StringLength(64)]
        public string Country { get; set; }

        public double? Lng { get; set; }

        public double? Lat { get; set; }

        public DateTime? LastUpdated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(128)]
        public string ClientGroupId { get; set; }

        [StringLength(512)]
        public string EntityName { get; set; }

        [StringLength(256)]
        public string EntityType { get; set; }

        [StringLength(256)]
        public string abn { get; set; }

        [StringLength(256)]
        public string acn { get; set; }

        [StringLength(50)]
        public string ClientType { get; set; }

        [StringLength(50)]
        public string EmploymentStatus { get; set; }

        public double ?  AnnualIncome { get; set; }

        public double ?  SuperAnnuationAmount { get; set; }


    }
}
