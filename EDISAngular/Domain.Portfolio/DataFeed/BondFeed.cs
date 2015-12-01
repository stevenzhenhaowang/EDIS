using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.DataFeed {
    public class BondFeed {
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public string Frequency { get; set; }
        public string BondType { get; set; }
        public string Issuer { get; set; }
    }
}
