namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DistributionList
    {
        public int DistributionListId { get; set; }

        [Column("DistributionList")]
        [StringLength(256)]
        public string DistributionList1 { get; set; }
    }
}
