using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Portfolio.DataFeed {
    public class LoanValueRatioFeed {
        public string Lender { get; set; }
        public AssetTypes AssetType { get; set; }
        public string Ticker { get; set; }
        public double Ratio { get; set; }
        public DateTime CreateOn { get; set; }
    }
}