using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.CorperateActions
{
    public class CorperateActionHistory
    {
        [Key]
        public int Id { get; set; }
        public CorporateActionStatus Status { get; set; }
        public CorporateActionType ActionType { get; set; }
        public string CorperateActionName { get; set; }
        public string AdviserId { get; set; } 
        public string CashAdjustmentAmount { get; set; }
        public string Ticker { get; set; }
        public string StockAdjustmentShareAmount { get; set; }
        public string AssociatedAccountId { get; set; }//This will be account Id for now not sure if it's right or not
        public string ClientGroupId { get; set; }//This is Client group Id
        public DateTime CorperateActionDate { get; set; }
    }
}
