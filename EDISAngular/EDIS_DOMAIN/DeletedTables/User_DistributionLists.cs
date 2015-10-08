namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_DistributionLists
    {
        public int Id { get; set; }

        public int? DistributionListId { get; set; }

        [StringLength(256)]
        public string UserId { get; set; }

        public DateTime? RetDate { get; set; }
    }
}
