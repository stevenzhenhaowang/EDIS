namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_InvestmentProfile
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public double? InvestableAssets { get; set; }

        public int? TimeHorizonId { get; set; }

        public double? ReturnsExpectation { get; set; }

        public int? InvestmentKnowledgeId { get; set; }

        public int? AttitudeToRiskId { get; set; }

        public int? AttitudeToCapitalLossId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
