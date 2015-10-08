using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace EDIS_DOMAIN
{
   public  class Accounts
    {
        [Key]
        public string AccountID { get; set; }
        public string ClientGroupID { get; set; }
        public string AccountNo { get; set; }
        public int AccountType { get; set; }
        public string Institution { get; set; }
        public string Note { get; set; }

    }
}
