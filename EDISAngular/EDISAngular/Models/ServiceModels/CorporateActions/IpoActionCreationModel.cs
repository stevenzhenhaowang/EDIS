using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class IpoActionCreationModel
    {
        [Required]
        public string nameOfRaising { get; set; }

        #region added property
        [Required]
        public string tickerNumber { get; set; }
        [Required]
        public string adviserUserId { get; set; }
        #endregion





        [Required]
        public string IPOCode { get; set; }
        [Required]
        public bool? listed { get; set; }
        [Required]
        public string exchange { get; set; }
        [Required]
        public DateTime? raisingOpened { get; set; }
        [Required]
        public DateTime? raisingClosed { get; set; }
        [Required]
        public DateTime? raisingTradingDate { get; set; }
        [Required]
        public DateTime? dispatchDocDate { get; set; }
        [Required]
        public double? issuedPrice { get; set; }
        [Required]
        public double? mimimumAmount { get; set; }
        [Required]
        public double? dividendPerShare { get; set; }
        [Required]
        public double? dividendYield { get; set; }
        [Required]
        public double? marketCapitalisation { get; set; }
        [Required]
        public double? raisingAmount { get; set; }
        [Required]
        public int? numberOfSharesOnIssue { get; set; }
        [Required]
        public int? numberOfSharesRaising { get; set; }
        [Required]
        public bool? allocationFinalised { get; set; }
        public List<IpoActionParticipantCreationModel> participants { get; set; }



    }

    public class IpoActionParticipantCreationModel
    {
        //must exist in db validator to be provided
        [Required]
        public string edisAccountNumber { get; set; }
        [Required]
        public string brokerAccountNumber { get; set; }
        [Required]
        public string brokerHinSrn { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double? investedAmount { get; set; }
        [Required]
        public int? numberOfUnits { get; set; }
        [Required]
        public int? unitPrice { get; set; }
        [Required]
        public string tickerNumber { get; set; }



    }




}