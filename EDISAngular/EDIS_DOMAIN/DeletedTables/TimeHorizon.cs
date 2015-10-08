namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeHorizon")]
    public partial class TimeHorizon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TimeHorizonId { get; set; }

        [Column("TimeHorizon")]
        [StringLength(64)]
        public string TimeHorizon1 { get; set; }
    }
}
