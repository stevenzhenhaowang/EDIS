namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_Subscriptions
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int? PlanTypeId { get; set; }

        public int? SubscriptionStatusId { get; set; }

        public double? AmountPaid { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndDate { get; set; }

        [StringLength(512)]
        public string TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }
    }
}
