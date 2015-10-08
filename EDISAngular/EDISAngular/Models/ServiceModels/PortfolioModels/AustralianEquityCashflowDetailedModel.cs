using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class EquityCashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<EquityCashflowDetailedItem> data { get; set; }
        public List<EquityCashflowDetailedValue> total { get; set; }
    }

    public class EquityCashflowDetailedItem
    {
        public string asx { get; set; }
        public string name { get; set; }
        public List<EquityCashflowDetailedValue> data { get; set; }
    }


    public class EquityCashflowDetailedValue
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }



}