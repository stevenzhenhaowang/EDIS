using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CashflowBriefModel
    {
        public List<CashFlowBriefItem> data { get; set; }
        public double totalIncome { get; set; }
        public double totalExpense { get; set; }
    }
    public class CashFlowBriefItem
    {
        public DateTime date { get; set; }
        public double income { get; set; }
        public double expense { get; set; }
        public string month { get; set; }
    }
}