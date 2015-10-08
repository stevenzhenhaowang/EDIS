using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class IPOActionData
    {
        public string actionId { get; set; }
        public string nameOfRaising { get; set; }
        public string IPOCode { get; set; }
        public bool listed { get; set; }
        public string exchange { get; set; }
        public DateTime raisingOpened { get; set; }
        public DateTime raisingClosed { get; set; }
        public DateTime raisingTradingDate { get; set; }
        public DateTime dispatchDocDate { get; set; }
        public double issuedPrice { get; set; }
        public double minimumAmount { get; set; }
        public double dividendPerShare { get; set; }
        public double dividendYield { get; set; }
        public double marketCapitalisation { get; set; }
        public double raisingAmount { get; set; }
        public int numberOfSharesOnIssue { get; set; }
        public int numberOfSharesRaising { get; set; }
        public bool allocationFinalised { get; set; }
        public List<IPOActionParticipant> participants { get; set; }


    }

    public class IPOActionParticipant
    {
        public string edisAccountNumber { get; set; }
        public string brokerAccountNumber { get; set; }
        public string brokerHinSrn { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public double investedAmount { get; set; }
        public int numberOfUnits { get; set; }
        public double unitPrice { get; set; }
        public string tickerNumber { get; set; }

    }




}