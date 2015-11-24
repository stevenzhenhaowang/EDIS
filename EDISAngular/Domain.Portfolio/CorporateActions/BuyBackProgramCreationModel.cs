using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class BuyBackProgramCreationModel
    {
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public string ActionName { get; set; }
        public DateTime BuyBackDate { get; set; }

        public List<BuyBackProgramParticipants> Participants { get; set; }

    }

    public class BuyBackProgramParticipants
    {
        public string AccountNumber { get; set; }
        public string ShareAmount { get; set; }
        public string CashAmount { get; set; }
    }
}

