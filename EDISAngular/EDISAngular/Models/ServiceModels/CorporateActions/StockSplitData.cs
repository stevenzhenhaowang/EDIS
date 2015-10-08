using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class StockSplitData
    {
        public string actionId { get; set; }
        public string actionCode { get; set; }
        public string stockSplitShares { get; set; }
        public DateTime splitDate { get; set; }
        public List<StockSplitParticipant> participants { get; set; }
    }
    public class StockSplitParticipant
    {
        public string edisAccountNumber { get; set; }
    }
}