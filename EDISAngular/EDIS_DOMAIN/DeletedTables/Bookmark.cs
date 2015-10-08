namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bookmark
    {
        public string BookmarkID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string BookmarkName { get; set; }

        [Required]
        [StringLength(100)]
        public string BookmarkLink { get; set; }

        [Required]
        [StringLength(500)]
        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
