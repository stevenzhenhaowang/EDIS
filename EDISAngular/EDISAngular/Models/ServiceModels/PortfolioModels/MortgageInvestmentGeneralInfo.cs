using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MortgageInvestmentGeneralInfo
    {
        public double marketValue { get; set; }
        public double outstandingLoans { get; set; }
        public double propertyGearingRatio { get; set; }
        public double monthlyRepayment { get; set; }
        public double annualInterestRepayment { get; set; }
        public double aveTermOfLoans { get; set; }
    }
}