namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_CV
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [Column(TypeName = "image")]
        public byte[] CV { get; set; }

        [StringLength(256)]
        public string OriginalFileName { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
