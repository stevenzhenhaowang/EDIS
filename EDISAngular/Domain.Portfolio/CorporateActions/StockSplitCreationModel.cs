using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class StockSplitCreationModel
    {
        public string ActionName { get; set; }
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public DateTime? splitDate { get; set; }
        public List<StockSplitParticipantAccounts> AccountsInfo { get; set; }
    }
    public class StockSplitParticipantAccounts
    {
        public string AccountNumber { get; set; }
        public string splitToUnit { get; set; }
    }
}

