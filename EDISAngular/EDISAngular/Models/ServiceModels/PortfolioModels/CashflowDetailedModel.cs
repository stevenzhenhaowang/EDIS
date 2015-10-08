using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<CashflowValue> total { get; set; }
        public List<CashflowDetailedItemGroup> data { get; set; }
    }


    public class CashflowDetailedItemGroup
    {
        public string type { get; set; }
        public List<CashflowValue> total { get; set; }
        public List<CashflowDetailedItem> items { get; set; }
    }

    public class CashflowDetailedItem
    {
        public List<CashflowValue> data { get; set; }
        public string name { get; set; }
    }

    public class CashflowValue
    {
        public double income { get; set; }
        public double expenses { get; set; }
    }




}