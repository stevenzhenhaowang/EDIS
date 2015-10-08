using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class FixedIncomeCashflowDetailedModel
    {
        public List<string> dateSchema { get; set; }
        public List<FixedIncomeCashflowDetailedItem> total { get; set; }
        public List<FixedIncomeCashflowDetailedGroup> data { get; set; }
    }


    public class FixedIncomeCashflowDetailedGroup
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public List<FixedIncomeCashflowDetailedItem> data { get; set; }
    }




    public class FixedIncomeCashflowDetailedItem
    {
        public double income { get; set; }
        public double franking { get; set; }
        public double expenses { get; set; }
    }



}