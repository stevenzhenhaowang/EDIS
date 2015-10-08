using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class HistoricalRevenueModel
    {
        public double total { get; set; }
        public List<HistoricalRevenueItem> data { get; set; }
    }
    public class HistoricalRevenueItem
    {
        public string title { get; set; }
        public double amount { get; set; }

    }
}