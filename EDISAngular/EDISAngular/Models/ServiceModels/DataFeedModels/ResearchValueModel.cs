using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.DataFeedModels {
    public class ResearchValueModel {
        public string AssetType { get; set; }
        public string Ticker { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Key { get; set; }
        public string ValueType { get; set; }
        public double Value { get; set; }
        public string StringValue { get; set; }
        public string Issuer { get; set; }
        public DateTime CreateDate { get; set; }
    }
}