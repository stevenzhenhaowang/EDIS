using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MortgageCashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<MortgageCashflowDetailedItem> total { get; set; }
        public List<MortgageCashflowDetailedGroup> data { get; set; }
    }
    public class MortgageCashflowDetailedGroup
    {
        public string propertyName { get; set; }
        public string address { get; set; }
        public List<MortgageCashflowDetailedItem> data { get; set; }

    }
    public class MortgageCashflowDetailedItem
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }


}