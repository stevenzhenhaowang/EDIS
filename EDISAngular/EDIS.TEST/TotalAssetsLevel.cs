namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TotalAssetsLevel")]
    public partial class TotalAssetsLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TotalAssetsLevelId { get; set; }

        [Column("TotalAssetsLevel")]
        [StringLength(256)]
        public string TotalAssetsLevel1 { get; set; }
    }
}
