using Edis.Db.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.CorporateAction
{
    public class ReturnOfCapitalAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ShareAmount { get; set; }
        [Required]
        public string ReturnOfCapitailAmount { get; set; }
        [Required]
        public DateTime? AdjustmentDate { get; set; }

        [Required]
        public virtual Equity Equities { get; set; }

     

        [Required]
        public virtual Adviser Adviser  { get; set; }

        public string AdviserId { get; set; }

       

        //mandatory corporate action all clients should be involved
        public virtual ICollection<Client> Clients { get; set; }
        
 
        public ReturnOfCapitalAction()
        {
            this.Clients = new HashSet<Client>();
        }


    }
}
