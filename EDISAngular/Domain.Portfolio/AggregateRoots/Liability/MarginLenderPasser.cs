using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.AggregateRoots.Liability {
    public class MarginLenderPasser {
        public string LenderId { get; set; }
        public string LenderName { get; set; }

        public List<LoanValueRatioPasser> Ratios { get; set; }
    }
}
