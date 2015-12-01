using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.DataFeed {
    public class ResearchValueFeed {
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
