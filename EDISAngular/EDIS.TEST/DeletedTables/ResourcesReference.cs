namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResourcesReference")]
    public partial class ResourcesReference
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string tokenValue { get; set; }

        [StringLength(512)]
        public string resourceUrl { get; set; }

        [StringLength(256)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string fileExtension { get; set; }
    }
}
