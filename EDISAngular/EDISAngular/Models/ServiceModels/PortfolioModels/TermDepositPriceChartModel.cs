using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class TermDepositPriceChartModel
    {
        public List<TermDepositPriceChartItem> data { get; set; }
    }
    public class TermDepositPriceChartItem
    {
        public DateTime date { get; set; }
        public double cashYield { get; set; }
        public double rba { get; set; }
        public string month { get; set; }
    }

}