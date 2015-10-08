namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Industry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IndustryID { get; set; }

        [Column("Industry")]
        [Required]
        [StringLength(200)]
        public string Industry1 { get; set; }

        public int SectorID { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
