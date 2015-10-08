namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_DealerGroupDetails
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string DealerGroupName { get; set; }

        [StringLength(16)]
        public string AFSL { get; set; }

        public int? HasDerivativesLicense { get; set; }

        public int? IsAuthorisedRep { get; set; }

        [StringLength(32)]
        public string AuthorisedRepNumber { get; set; }

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

        public DateTime? LastUpdated { get; set; }
    }
}
