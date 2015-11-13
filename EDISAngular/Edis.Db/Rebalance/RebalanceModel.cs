using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.Rebalance
{
    public class RebalanceModel
    {
        [Key]
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public int ProfileId { get; set; }

        public virtual ICollection<TemplateDetailsItemParameter> TemplateDetailsItemParameters { get; set; }
        public virtual Adviser Adviser { get; set; }
        public virtual ClientGroup ClientGroup { get; set; }
        public virtual Client Client{ get; set; }

        public RebalanceModel()
        {
            this.TemplateDetailsItemParameters = new HashSet<TemplateDetailsItemParameter>();
        }
    }
}
