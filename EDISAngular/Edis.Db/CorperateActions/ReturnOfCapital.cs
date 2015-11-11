using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.CorperateActions
{
    public class ReturnOfCapital
    {
        [Key]
        public int Id { get; set; }
        public string AdviserId { get; set; }
        public string CorperateActionName { get; set; }
        public string ReturnCashAmount { get; set; }
        public string  AssociatedAccountNumber { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
