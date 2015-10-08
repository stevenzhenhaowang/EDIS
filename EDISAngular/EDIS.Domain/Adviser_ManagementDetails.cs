namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_ManagementDetails
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public double? TotalAssets { get; set; }

        public double? TotalManagedInvestments { get; set; }

        public double? TotalDirectAustralianEquities { get; set; }

        public double? TotalDirectInternational { get; set; }

        public double? TotalFixedInterest { get; set; }

        public double? TotalLendingBook { get; set; }

        public int? ApproxClientNumbersId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
