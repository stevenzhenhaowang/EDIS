using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.DataFeedModels {
    public class AssetPriceModel {
        public string Ticker { get; set; }
        public string Address { get; set; }
        public double AssetPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AssetType { get; set; }
    }
}