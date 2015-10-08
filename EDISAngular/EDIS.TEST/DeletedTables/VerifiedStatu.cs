namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VerifiedStatu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VerifiedStatusId { get; set; }

        [StringLength(32)]
        public string VerifiedStatus { get; set; }
    }
}
