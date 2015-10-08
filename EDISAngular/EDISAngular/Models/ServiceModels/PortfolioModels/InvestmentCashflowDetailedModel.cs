using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class InvestmentCashflowDetailedModel
    {

        public List<string> dateSchema { get; set; }
        public List<InvestmentCashflowDetailedItem> data { get; set; }
        public List<InvestmentCashflowDetailedValue> total { get; set; }

    }
    public class InvestmentCashflowDetailedItem
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public List<InvestmentCashflowDetailedValue> data { get; set; }
    }


    public class InvestmentCashflowDetailedValue
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }
}