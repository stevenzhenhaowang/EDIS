using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.Rebalance
{
    public class TemplateDetailsItemParameter
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string EquityId { get; set; }
        [Required]
        public string ItemName { get; set; }

        public double? MarketValue { get; set; }
        public double? CurrentValue { get; set; }
        public double? CurrentWeighting { get; set; }
        public string identityMetaKey { get; set; }

        public virtual RebalanceModel Model { get; set; }
    }
}
