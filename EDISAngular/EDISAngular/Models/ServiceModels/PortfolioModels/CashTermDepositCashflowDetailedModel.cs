using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CashTermDepositCashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<CashTermDepositCashflowDetailedItem> total { get; set; }
        public List<CashTermDepositCashflowDetailedGroup> data { get; set; }
    }


    public class CashTermDepositCashflowDetailedGroup
    {
        public string type { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public List<CashTermDepositCashflowDetailedItem> data { get; set; }


    }

    public class CashTermDepositCashflowDetailedItem
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }


}