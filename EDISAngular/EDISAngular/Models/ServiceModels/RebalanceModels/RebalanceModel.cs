using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class RebalanceModel
    {
        public string modelId { get; set; }
        public string modelName { get; set; }
        public ModelProfile profile { get; set; }

        public List<DiversificationDatas> diversificationData { get; set; }
        public BalanceSheetAgainstModel balanceSheet { get; set; }
        public List<RebalanceDataAnalysisModel> rebalancedDataAnalysis { get; set; }
        public RegionalComparisonModel regionalData { get; set; }
        public SectorialComparisonModel sectorialData { get; set; }
        public MonthlyCashflowComparison monthlyCashflow { get; set; }
        public MonthlyInvestmentIncomeModel monthlyInvestmentIncome { get; set; }
        public AnticipatedCostModel anticipatedFirstYearManagementCost { get; set; }
        public AnticipatedCostModel anticipatedSecondYearManagementCost { get; set; }
        public AnticipatedCostModel anticipatedFirstYearOngoingServiceFee { get; set; }
        public AnticipatedCostModel anticipatedSecondYearOngoingServiceFee { get; set; }
        public List<TransactionCostData> transactionCost { get; set; }
        public List<ComplianceData> compliance { get; set; }
        public TemplateDetails templateDetails { get; set; }
    }
}