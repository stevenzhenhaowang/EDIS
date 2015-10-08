namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NoteLink
    {
        [Key]
        [Column(Order = 0)]
        public string NoteID1 { get; set; }

        [Key]
        [Column(Order = 1)]
        public string NoteID2 { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Note Note { get; set; }

        public virtual Note Note1 { get; set; }
    }
}
