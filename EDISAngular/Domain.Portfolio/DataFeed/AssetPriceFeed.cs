using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.DataFeed {
    public class AssetPriceFeed {
        public string Ticker { get; set; }
        public string Address { get; set; }
        public double AssetPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AssetType { get; set; }
    }
}
