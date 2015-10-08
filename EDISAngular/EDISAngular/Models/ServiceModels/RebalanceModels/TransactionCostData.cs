using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class TransactionCostData
    {

        public string assetClass { get; set; }
        public List<TransactionCostDataItem> items { get; set; }
        public double buySell { get; set; }
        public double profitLoss { get; set; }
        public double transactionCost { get; set; }
        public double extraDividend { get; set; }
        public double extraMER { get; set; }
        public double netValue { get; set; }
    }


    public class TransactionCostDataItem
    {
        public string name { get; set; }
        public double buySell { get; set; }
        public double profitLoss { get; set; }
        public double transactionCost { get; set; }
        public double extraDividend { get; set; }
        public double extraMER { get; set; }
        public double netValue { get; set; }
    }
}