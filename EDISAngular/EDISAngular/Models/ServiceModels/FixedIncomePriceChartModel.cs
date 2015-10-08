using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels
{
    public class CashPriceChartModel
    {
        public List<CashPriceChartItem> data { get; set; }
    }

    public class CashPriceChartItem
    {
        public DateTime date { get; set; }
        public double bondYield { get; set; }
        public double rba { get; set; }
        public string month { get; set; }

    }


}