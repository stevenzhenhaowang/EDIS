namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_CommissionsAndFees
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? CommissionsAndFeesId { get; set; }

        public int? YesNoNA { get; set; }

        [StringLength(500)]
        public string CommissionDescription { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
