using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class FixedIncomeProfileModel
    {
        public List<FixedIncomeProfileItem> data { get; set; }
    }

    public class FixedIncomeProfileItem
    {
        public string ticker { get; set; }
        public string fixedIncomeName { get; set; }
        public string currency { get; set; }
        public double faceValue { get; set; }
        public double coupon { get; set; }
        public string couponFrequency { get; set; }
        public string issuer { get; set; }
        public double costValue { get; set; }
        public double brokerage { get; set; }
        public double totalCostValue { get; set; }
        public double totalCostValueAUD { get; set; }
        public double totalCostValueWeighting { get; set; }
        public double totalCostValueAssetAllocationWeighting { get; set; }
        public double marketPrice { get; set; }
        public double marketValue { get; set; }
        public double marketValueAUD { get; set; }
        public double marketValueWeighting { get; set; }
        public double marketValueAssetAllocationWeighting { get; set; }
        public double incomeSuitabilityToInvestor { get; set; }
        public string instrumentType { get; set; }
        public double bondTerm { get; set; }
        public DateTime maturityDate { get; set; }
        //public double faceValue { get; set; }
        public double presentValue { get; set; }
        public double yieldToMaturity { get; set; }
        public double couponRate { get; set; }
        //public string couponFrequency { get; set; }
        public double interestAccrued { get; set; }
        public double bondRating { get; set; }
        public string ratingAgency { get; set; }
        public int priority { get; set; }
        public string redemptionFeatures { get; set; }




    }

}