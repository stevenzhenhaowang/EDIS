using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ViewModels
{
    public class ProfessionType
    {
        public int ProfessionTypeId { get; set; }
        public string ProfessionType1 { get; set; }
    }

    public class EducationLevel
    {
        public int EducationLevelsId { get; set; }
        public string EducationLevels { get; set; }
    }

    public class NewsletterService
    {
        public int NewsletterServicesId { get; set; }
        public string NewsletterServices { get; set; }
    }


    public class CommissionsAndFee
    {
        public int CommissionsAndFeesId { get; set; }
        public string CommissionsAndFees { get; set; }
    }

    public class ApproxClientNumber
    {
        public int ApproxClientNumbersId { get; set; }
        public string ApproxClientNumbers { get; set; }
    }

    public class InvestibleAssetsLevel
    {
        public int InvestibleAssetsLevelId { get; set; }
        public string InvestibleAssetsLevel1 { get; set; }
    }
    public class TotalAssetsLevel
    {
        public int TotalAssetsLevelId { get; set; }
        public string TotalAssetsLevel1 { get; set; }
    }

    public class AnnualIncomeLevel
    {
        public int AnnualIncomeLevelsId { get; set; }
        public string AnnualIncomeLevels { get; set; }
    }

}