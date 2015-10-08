using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class BonusIssueData
    {

            public string actionId { get; set; }
            public string actionCode { get; set; }
            public string bonusIssue { get; set; }
            public DateTime bonusDate { get; set; }
            public List<BonusParticipant> partcipants { get; set; }
        
    }

    public class BonusParticipant
    {
        public string edisAccountNumber { get; set; }
    }
}