using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class BonusIssueCreationModel
    {
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public string ActionName { get; set; }
        public DateTime BonusIssueDate { get; set; }

        public List<BonusIssueParticipants> Participants { get; set; }

    }

    public class BonusIssueParticipants
    {
        public string AccountNumber { get; set; }
        public string ShareAmount { get; set; }
    }
}
