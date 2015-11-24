using Edis.Db.Liabilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db {
    public class MarginLender {
        [Key]
        public string LenderId { get; set; }
        public string LenderName { get; set; }

        public virtual ICollection<LoanValueRatio> Ratios { get; set; }

        public MarginLender() {
            Ratios = new HashSet<LoanValueRatio>();
        }
    }
}
