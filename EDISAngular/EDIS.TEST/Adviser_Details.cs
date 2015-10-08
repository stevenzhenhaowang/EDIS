namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_Details
    {
        [Key]
        public string AdvisorUserId { get; set; }

        [StringLength(64)]
        public string CurrentTitle { get; set; }

        [StringLength(64)]
        public string CompanyName { get; set; }

        [StringLength(16)]
        public string ABNACN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ExperienceStartDate { get; set; }

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

        public int? VerifiedId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        public byte[] Image { get; set; }

        [StringLength(256)]
        public string ImageMimeType { get; set; }
    }
}
