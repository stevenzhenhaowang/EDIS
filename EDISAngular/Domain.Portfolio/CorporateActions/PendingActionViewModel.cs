using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class PendingActionViewModel
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionType { get; set; }
        public string Ticker { get; set; }
        public string ShareAmount { get; set; }
        //public string Price { get; set; }
        public string CashAdjustments { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
    }
}
