namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin_Posts
    {
        public int Id { get; set; }

        [StringLength(512)]
        public string PostHeading { get; set; }

        [StringLength(256)]
        public string PostAuthor { get; set; }

        [Column(TypeName = "image")]
        public byte[] PostImageData { get; set; }

        public DateTime? PostDate { get; set; }

        public string PostContents { get; set; }

        public int? PostTypeId { get; set; }
    }
}
