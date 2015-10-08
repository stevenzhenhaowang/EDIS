using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EDISAngular.Models.ModelValidators;
using System.Web.Mvc;



namespace EDISAngular.Models.BindingModels
{
    public class RiskProfileBindingModel
    {
        [HiddenInput(DisplayValue = false)]
        public string profileId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string clientId { get; set; }



        [DisplayName("Primary Short Term Goal")]
        public string shortTermGoal1 { get; set; }

        [DisplayName("Secondary Short Term Goal")]
        public string shortTermGoal2 { get; set; }

        [DisplayName("Third Short Term Goal")]
        public string shortTermGoal3 { get; set; }


        [DisplayName("Primary Medium Term Goal")]
        public string medTermGoal1 { get; set; }

        [DisplayName("Secondary Medium Term Goal")]
        public string medTermGoal2 { get; set; }

        [DisplayName("Third Medium Term Goal")]
        public string medTermGoal3 { get; set; }


        [DisplayName("Primary Long Term Goal")]
        public string longTermGoal1 { get; set; }

        [DisplayName("Secondary Long Term Goal")]
        public string longTermGoal2 { get; set; }

        [DisplayName("Third Long Term Goal")]
        public string longTermGoal3 { get; set; }

        [DisplayName("Additional Commentary")]
        public string comments { get; set; }



        [Range(0, int.MaxValue,ErrorMessage="Please enter a valid age")]
        [DisplayName("Age of Retirement")]      
        public string retirementAge { get; set; }
        [Range(0, double.MaxValue)]
        [DisplayName("Income Needed in Retirement Per Annum")]
        public double? retirementIncome { get; set; }
        [DisplayName("Sources if Income")]
        public string incomeSource { get; set; }

        [DisplayName("Primary Investment Objective")]
        public string investmentObjective1 { get; set; }

        [DisplayName("Secondary Investment Objective")]
        public string investmentObjective2 { get; set; }

        [DisplayName("Third Investment Objective")]
        public string investmentObjective3 { get; set; }


        [Range(0, int.MaxValue)]
        [DisplayName("Desired Investment Time Horizon")]
        public int? investmentTimeHorizon { get; set; }

        [DisplayName("Description of Investment Knowledge")]
        public string investmentKnowledge { get; set; }

        [DisplayName("Description of Attitude to Risk")]
        public string riskAttitude { get; set; }

        [DisplayName("Description of Attitude to Capital Loss")]
        public string capitalLossAttitude { get; set; }

        [DisplayName("Short Term Trading")]
        public string shortTermTrading { get; set; }
        [Range(0, double.MaxValue)]
        [DisplayName("% of Total Asset for Short Term Trading")]
        public double? shortTermAssetPercent { get; set; }
        [Range(0, double.MaxValue)]
        [DisplayName("% of Domestic Equity only for Short Term Trading")]
        public double? shortTermEquityPercent { get; set; }

        [DisplayName("Investment Profile")]
        public string investmentProfile { get; set; }

    }
}