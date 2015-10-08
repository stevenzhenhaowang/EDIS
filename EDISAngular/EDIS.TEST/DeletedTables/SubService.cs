namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubServiceId { get; set; }

        [StringLength(128)]
        public string SubServiceName { get; set; }

        public int? ServiceId { get; set; }
    }
}
