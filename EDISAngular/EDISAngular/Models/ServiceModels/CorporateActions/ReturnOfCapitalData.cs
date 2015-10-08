using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class ReturnOfCapitalData
    {

        public string actionId { get; set; }
        public string actionName { get; set; }
        public string equityId { get; set; }
        public string shareAmount { get; set; }
        public string returnAmount { get; set; }
        public DateTime returnDate { get; set; }
        public List<returnOfCapitalParticipant> participants { get; set; }
    }


    public class returnOfCapitalParticipant
    {
        public string edisAccountNumber { get; set; }
    }


}
