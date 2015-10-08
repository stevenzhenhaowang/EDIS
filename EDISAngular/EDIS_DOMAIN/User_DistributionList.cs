namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_DistributionList
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? DistributionListId { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
