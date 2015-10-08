namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttitudeToCapitalLoss")]
    public partial class AttitudeToCapitalLoss
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttitudeToCapitalLossId { get; set; }

        [Column("AttitudeToCapitalLoss")]
        [StringLength(64)]
        public string AttitudeToCapitalLoss1 { get; set; }
    }
}
