namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubscriptionStatu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubscriptionStatusId { get; set; }

        [StringLength(256)]
        public string SubscriptionStatus { get; set; }
    }
}
