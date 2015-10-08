namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeFrameForAdvice")]
    public partial class TimeFrameForAdvice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TimeFrameForAdviceId { get; set; }

        [Column("TimeFrameForAdvice")]
        [StringLength(32)]
        public string TimeFrameForAdvice1 { get; set; }
    }
}
