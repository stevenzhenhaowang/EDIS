using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.EdisDatabase {
    public class RiskProfile {

        public string RiskProfileID { get; set; }
        public string ClientID { get; set; }

        public string CapitalLossAttitude { get; set; }
        public string Comments { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string IncomeSource { get; set; }
        public string InvestmentKnowledge { get; set; }
        public string InvestmentObjective1 { get; set; }
        public string InvestmentObjective2 { get; set; }
        public string InvestmentObjective3 { get; set; }
        public string InvestmentProfile { get; set; }
        public int? InvestmentTimeHorizon { get; set; }
        public string LongTermGoal1 { get; set; }
        public string LongTermGoal2 { get; set; }
        public string LongTermGoal3 { get; set; }
        public string MedTermGoal1 { get; set; }
        public string MedTermGoal2 { get; set; }
        public string MedTermGoal3 { get; set; }
        public int? RetirementAge { get; set; }
        public double? RetirementIncome { get; set; }
        public string RiskAttitude { get; set; }
        public double? ShortTermAssetPercent { get; set; }
        public double? ShortTermEquityPercent { get; set; }
        public string ShortTermGoal1 { get; set; }
        public string ShortTermGoal2 { get; set; }
        public string ShortTermGoal3 { get; set; }
        public string ShortTermTrading { get; set; }

        public int riskLevel { get; set; }
    }
}
