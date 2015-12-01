using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.DataFeedModels {
    public class LoanValueRatioModel {
        public string Lender { get; set; }
        public string AssetType { get; set; }
        public string Ticker { get; set; }
        public double Ratio { get; set; }
        public DateTime CreateOn { get; set; }
    }
}