using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Rebalance
{
    public class TemplateDetailsItemParameter
    {
        public string EquityId { get; set; }
        public string ItemName { get; set; }
        public double? MarketValue { get; set; }
        public double? CurrentValue { get; set; }
        public double? CurrentWeighting { get; set; }
        public string identityMetaKey { get; set; }
        public string ModelId { get; set; }
    }
}
