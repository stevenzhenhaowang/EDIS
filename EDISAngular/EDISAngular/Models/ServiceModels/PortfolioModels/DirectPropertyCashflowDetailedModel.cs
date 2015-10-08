using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class DirectPropertyCashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<DirectPropertyCashflowDetailedItem> data { get; set; }
        public List<DirectPropertyCashflowDetailedValue> total { get; set; }
    }

    public class DirectPropertyCashflowDetailedItem
    {
        public string propertyName { get; set; }
        public string address { get; set; }
        public List<DirectPropertyCashflowDetailedValue> data { get; set; }
    }


    public class DirectPropertyCashflowDetailedValue
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }


}