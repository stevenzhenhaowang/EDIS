using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class OtherActionCreationModel
    {
        [Required]
        public string corporateActionName { get; set; }

        #region added property
        [Required]
        public string adviserUserId { get; set; }
        #endregion


        [Required]
        public string corporateActionCode { get; set; }
        [Required]
        public string purposeForCorporateAction { get; set; }
        [Required]
        public OtherActionUnderlyingCompanyCreationModel underlyingCompany { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? recordDateEntitlement { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? exEntitlement { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? corporateActionStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? corporateActionClosingDate { get; set; }
        [Required]
        public string dispatchOfHolding { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? deferredSettlementTradingDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? normalTradingDate { get; set; }
        [Required]
        public List<OtherActionParticipantCreationModel> participants { get; set; }




    }


    public class OtherActionUnderlyingCompanyCreationModel
    {
        [Required]
        public string companyTicker { get; set; }
        [Required]
        public string name { get; set; }
    }


    public class OtherActionParticipantCreationModel
    {
        [Required]
        public string  edisAccountNumber { get; set; }
        [Required]
        public string brokerAccountNumber { get; set; }
        [Required]
        public string brokerHinSrn { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string name { get; set; }

    }




}