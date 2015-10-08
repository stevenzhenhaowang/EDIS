using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class PortfolioSummary
    {
        public SummaryItem investment { get; set; }
        public SummaryItem liability { get; set; }
        public SummaryItem networth { get; set; }
    }


    public class SummaryItem
    {
        public List<DataNameAmountPair> data { get; set; }
        public double total { get; set; }
    }

    public class SummaryGeneralInfo
    {
        public double cost { get; set; }
        public double marketValue { get; set; }
        public double pl { get; set; }//profit/loss
        public double plp { get; set; }//profit/loss percentage
    }

}