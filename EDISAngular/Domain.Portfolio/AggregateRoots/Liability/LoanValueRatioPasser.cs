using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.AggregateRoots.Liability {
    public class LoanValueRatioPasser {
        public string Id { get; set; }
        public double MaxRatio { get; set; }
        public string Ticker { get; set; }
        public AssetTypes AssetTypes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ActiveDate { get; set; }
    }
}
