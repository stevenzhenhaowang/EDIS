using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels {
    public class MarginLendingAccountPortfolioDetails {
        public List<MarginLendingAccountPortfolioItem> data { get; set; }
        public List<MarginLendersDetails> marginLenders { get; set; }
    }

    public class MarginLendingAccountPortfolioItem {
        public string ticker { get; set; }
        public string companyName { get; set; }
        public double marketValue { get; set; }
        public double netCostValue { get; set; }
        public double loanAmount { get; set; }
        public double loanValueRatio { get; set; }
        public double maxLoanValueRatio { get; set; }
    }

    public class MarginLendersDetails {
        public string ticker { get; set; }
        public string companyName { get; set; }
        public double maxLoanValueRatio { get; set; }
    }
}