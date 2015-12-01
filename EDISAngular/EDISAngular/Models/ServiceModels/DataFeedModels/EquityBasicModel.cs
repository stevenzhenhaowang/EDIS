using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.DataFeedModels {
    public class EquityBasicModel {
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public string Sector { get; set; }
        public string EquityType { get; set; }
    }
}