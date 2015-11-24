using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class ClientCorporateHistoryModel
    {
        public int ReferenceID { get; set; }
        public CorporateActionStatus Status { get; set; }
        public CorporateActionType ActionType { get; set; }
        public string CorperateActionName { get; set; }
        public string AdviserId { get; set; }
        public string CashAdjustmentAmount { get; set; }
        public string Ticker { get; set; }
        public string StockAdjustmentShareAmount { get; set; }
        //public string BeforeActionShares { get; set; }//maybe this will be useful but I  do not want to implement for now
        public string AssociatedAccountNumber { get; set; }//This will be account Id for now which can also uique identify account
        //public string ClientGroupId { get; set; }//This is Client group Id
        public DateTime CorperateActionDate { get; set; }
    }
}
