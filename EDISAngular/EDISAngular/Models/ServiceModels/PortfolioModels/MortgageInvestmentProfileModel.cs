using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MortgageInvestmentProfileModel
    {
        public List<MortgageInvestmentProfileItem> data { get; set; }
    }

    public class MortgageInvestmentProfileItem
    {
        public string propertyName { get; set; }
        public string address { get; set; }
        public string currency { get; set; }
        public double marketValue { get; set; }
        public double outstandingLoan { get; set; }
        public double currentPropertyGearingRatio { get; set; }
        public string institution { get; set; }
        public string typeOfRates { get; set; }
        public double interestRates { get; set; }
        public double monthlyRepaymentAmount { get; set; }
        public int montlyRepaymentDate { get; set; }
        public double loanContractTerm { get; set; }
        public DateTime startDate { get; set; }
        public DateTime loanExpiryDate { get; set;}
        public double numberOfYearsToDate { get; set; }
        public double NumberOfYearsToExpiry { get; set; }
        public string RepaymentType { get; set; }
        public double maximumLoanLimit { get; set; }
        public double currentLoanBalance { get; set; }
        public double avaliableFunds { get; set; }
        public string repaymentMethod { get; set; }
        public double currentFinancialYearInterest { get; set; }
        public double lastFinancialYearInterest { get; set; }
        public double suitability { get; set; }
        public string id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string type { get; set; }
    }

}