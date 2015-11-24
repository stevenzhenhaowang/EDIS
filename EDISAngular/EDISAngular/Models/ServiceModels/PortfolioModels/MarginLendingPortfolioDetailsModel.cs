using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels {
    public class MarginLendingPortfolioDetailsModel {
        public List<MarginLendingPortfolioDetailsItem> data { get; set; }
    }

    public class MarginLendingPortfolioDetailsItem{
        
        public string assetCatargory { get; set; }
        public double marketValue { get; set; }
        public double netCostValue { get; set; }
        public double loanAmount { get; set; }
        public double loanValueRatio { get; set; }
        public double maxLoanValueRatio { get; set; }
    }
}