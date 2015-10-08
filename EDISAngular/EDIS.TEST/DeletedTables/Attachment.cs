namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attachment
    {
        public string AttachmentID { get; set; }

        [StringLength(128)]
        public string NoteID { get; set; }

        public string Path { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        public byte[] Data { get; set; }

        [StringLength(100)]
        public string AttachmentType { get; set; }

        public virtual Note Note { get; set; }
    }
}
