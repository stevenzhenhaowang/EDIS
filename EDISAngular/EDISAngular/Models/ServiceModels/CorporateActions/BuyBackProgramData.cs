using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class BuyBackProgramData
    {
     
            public string actionId { get; set; }
            public string cashAdjustments { get; set; }
            public string stockAdjustments { get; set; }
            public DateTime buyBackTime { get; set; }
            public List<BuybackParticipant> participants { get; set; }
        
    }

    public class BuybackParticipant
    {
        public string edisAccountNumber { get; set; }
    }
}