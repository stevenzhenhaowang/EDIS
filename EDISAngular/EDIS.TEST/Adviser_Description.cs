namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_Description
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(1000)]
        public string AdviserDescription { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int IsApproved { get; set; }
    }
}
