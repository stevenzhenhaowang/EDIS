using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class RebalanceDataAnalysisModel
    {
        public string groupName { get; set; }
        public List<RebalanceDataAnalysisData> data { get; set; }
    }

    public class RebalanceDataAnalysisData
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public double currentPrice { get; set; }
        public RebalanceDataAnalysisDataItem currentExposure { get; set; }
        public RebalanceDataAnalysisDataItem rebalance { get; set; }
        public RebalanceDataAnalysisAdvantageDisadvantage advantageousAndDisadvantageous { get; set; }
    }

    public class RebalanceDataAnalysisDataItem
    {
        public int units { get; set; }
        public double value { get; set; }
        public double profitAndLoss { get; set; }
    }

    public class RebalanceDataAnalysisAdvantageDisadvantage
    {
        public double differences { get; set; }
        public double suitability { get; set; }
        public double target { get; set; }
        public double differenceToTarget { get; set; }
        public string apl { get; set; }
    }



}