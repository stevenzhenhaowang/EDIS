namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubscriptionPlanType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanTypeId { get; set; }

        [StringLength(256)]
        public string PlanType { get; set; }
    }
}
