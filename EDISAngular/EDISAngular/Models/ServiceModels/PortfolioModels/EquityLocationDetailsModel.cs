using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels {
    public class EquityLocationDetailsModel {
        public string countryCodes { get; set; }
        public List<EquityDetails> data { get; set; }
    }

    public class EquityDetails {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string EquityType { get; set; }
        public int NumberOfUnit { get; set; }
        public double MarketValue { get; set; }
    }
}