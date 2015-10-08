namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_ProfilePictures
    {
        [StringLength(128)]
        public string UserId { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImageData { get; set; }

        [StringLength(128)]
        public string UserTypeId { get; set; }

        public int? PictureApprovalStatusId { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int id { get; set; }
    }
}
