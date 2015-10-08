using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{



    public class OtherCorporateActionData
    {
        public string actionId { get; set; }
        public string corporateActionName { get; set; }
        public string corporateActionCode { get; set; }
        public string purposeForCorporateAction { get; set; }
        public OtherCorporateActionCompany underlyingCompany { get; set; }
        public DateTime recordDateEntitlement { get; set; }
        public DateTime exEntitlement { get; set; }
        public DateTime corporateActionStartDate { get; set; }
        public DateTime corporateActionClosingDate { get; set; }
        public string dispatchOfHolding { get; set; }
        public DateTime deferredSettlementTradingDate { get; set; }
        public DateTime normalTradingDate { get; set; }
        public List<OtherActionParticipant> participants { get; set; }
    }

    public class OtherCorporateActionCompany
    {
        public string companyTicker { get; set; }
        public string name { get; set; }
    }

    public class OtherActionParticipant
    {
        public string edisAccountNumber { get; set; }
        public string brokerAccountNumber { get; set; }
        public string brokerHinSrn { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }
}