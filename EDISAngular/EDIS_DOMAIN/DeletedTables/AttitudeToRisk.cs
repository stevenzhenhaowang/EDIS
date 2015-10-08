namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttitudeToRisk")]
    public partial class AttitudeToRisk
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttitudeToRiskId { get; set; }

        [Column("AttitudeToRisk")]
        [StringLength(128)]
        public string AttitudeToRisk1 { get; set; }
    }
}
