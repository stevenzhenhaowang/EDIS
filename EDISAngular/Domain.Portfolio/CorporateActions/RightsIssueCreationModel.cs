using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class RightsIssueCreationModel
    {
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public string ActionName { get; set; }
        public DateTime RightsIssueDate { get; set; }

        public List<RightsIssueParticipants> Participants { get; set; }

    }

    public class RightsIssueParticipants
    {
        public string AccountNumber { get; set; }
        public string ShareAmount { get; set; }
        public string CashAmount { get; set; }
    }
}

