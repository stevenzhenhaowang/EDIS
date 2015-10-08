namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_Verified_Status
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? VerifiedStatusId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
