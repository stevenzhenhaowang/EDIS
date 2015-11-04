using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Rebalance
{
    public class RebalanceModel
    {
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public int ProfileId { get; set; }
        public string ClientGroupId { get; set; }
        public string AdviserId { get; set; }

        public List<TemplateDetailsItemParameter> TemplateDetailsItemParameters { get; set; }
    }
}
