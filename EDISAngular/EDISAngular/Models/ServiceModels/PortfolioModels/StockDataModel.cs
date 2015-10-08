using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class StockDataModel
    {
        public int year { get; set; }
        public double asxbhp { get; set; }
        public double nysewbc { get; set; }
        public string AssetName { get; set; }
        public double asxtls { get; set; }
        public double AssetUnitPrice { get; set; }
    }
}