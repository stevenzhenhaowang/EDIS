using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class ReinvestmentPlanCreationModel
    {
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public string ActionName  { get; set; }
        public DateTime ReinvestmentDate { get; set; }
        
        public List<ReinvestmentPlanParticipants> Participants { get; set; }

    }

    public class ReinvestmentPlanParticipants {
        public string AccountNumber { get; set; }
        public string ShareMount { get; set; }
    }
}
