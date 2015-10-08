namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_FinancialPosition
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? TimeFrameForAdviceId { get; set; }

        public int? EmploymentStatusId { get; set; }

        public double? TotalAnnualIncome { get; set; }

        public double? TotalAssets { get; set; }

        public double? TotalLiabilities { get; set; }

        public double? TotalEquity { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
