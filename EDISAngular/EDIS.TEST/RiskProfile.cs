namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RiskProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RiskProfile()
        {
        }

        public string RiskProfileID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        [StringLength(200)]
        public string ShortTermGoal1 { get; set; }

        [StringLength(200)]
        public string ShortTermGoal2 { get; set; }

        [StringLength(200)]
        public string ShortTermGoal3 { get; set; }

        [StringLength(200)]
        public string MedTermGoal1 { get; set; }

        [StringLength(200)]
        public string MedTermGoal2 { get; set; }

        [StringLength(200)]
        public string MedTermGoal3 { get; set; }

        [StringLength(200)]
        public string LongTermGoal1 { get; set; }

        [StringLength(200)]
        public string LongTermGoal2 { get; set; }

        [StringLength(200)]
        public string LongTermGoal3 { get; set; }

        public string Comments { get; set; }

        public int? RetirementAge { get; set; }

        public double? RetirementIncome { get; set; }

        [StringLength(100)]
        public string IncomeSource { get; set; }

        [StringLength(200)]
        public string InvestmentObjective1 { get; set; }

        [StringLength(200)]
        public string InvestmentObjective2 { get; set; }

        [StringLength(200)]
        public string InvestmentObjective3 { get; set; }

        public int? InvestmentTimeHorizon { get; set; }

        [StringLength(200)]
        public string InvestmentKnowledge { get; set; }

        [StringLength(200)]
        public string RiskAttitude { get; set; }

        [StringLength(200)]
        public string CapitalLossAttitude { get; set; }

        [StringLength(50)]
        public string ShortTermTrading { get; set; }

        public double? ShortTermAssetPercent { get; set; }

        public double? ShortTermEquityPercent { get; set; }

        [StringLength(100)]
        public string InvestmentProfile { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public virtual Client Client { get; set; }

    }
}
