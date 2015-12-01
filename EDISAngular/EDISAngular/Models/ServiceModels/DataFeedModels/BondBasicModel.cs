using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.DataFeedModels {
    public class BondBasicModel {
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public string Frequency { get; set; }
        public string BondType { get; set; }
        public string Issuer { get; set; }
    }
}