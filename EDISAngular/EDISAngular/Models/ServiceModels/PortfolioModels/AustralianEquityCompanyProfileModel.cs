using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class EquityCompanyProfileModel
    {
        public List<EquityCompanyProfileItemModel> data { get; set; }
        public double totalCostInvestment { get; set; }
        public double totalCostWeighting { get; set; }
        public double totalMarketValue { get; set; }
        public double totalMarketValueWeighting { get; set; }
        public double totalpl { get; set; }
    }
    public class EquityCompanyProfileItemModel
    {
        public string asx { get; set; }
        public string company { get; set; }
        public double costValue { get; set; }
        public double brokerage { get; set; }
        public double totalCostValue { get; set; }
        public double totalCostValueWeighting { get; set; }
        public double marketPrice { get; set; }
        public double marketValue { get; set; }
        public double marketValueWeighting { get; set; }
        public double marketValueAssetAllocationWeighting { get; set; }
        public double companySuitabilityToInvestor { get; set; }
        public double earningsPerShare { get; set; }
        public double earningsPerShareWeighting { get; set; }
        public double earningsPerShareGrowth { get; set; }
        public double earningsPerShareGrowthWeighting { get; set; }
        public double dividend { get; set; }
        public double dividendWeighting { get; set; }
        public double franking { get; set; }
        public double frankingWeighting { get; set; }
        public double dividendYield { get; set; }
        public double dividendYieldWeighting { get; set; }
        public double priceEarningsRatio { get; set; }
        public double priceEarningsRatioWeighting { get; set; }
        public double returnOnAsset { get; set; }
        public double returnOnAssetWeighting { get; set; }
        public double returnOnEquity { get; set; }
        public double returnOnEquityWeighting { get; set; }
        public double oneYearReturn { get; set; }
        public double oneYearReturnWeighting { get; set; }
        public double threeYearReturn { get; set; }
        public double threeYearReturnWeighting { get; set; }
        public double fiveYearReturn { get; set; }
        public double fiveYearReturnWeighting { get; set; }
        public double beta { get; set; }
        public double betaWeighting { get; set; }
        public double currentRatio { get; set; }
        public double currentRatioWeighting { get; set; }
        public double quickRatio { get; set; }
        public double quickRatioWeighting { get; set; }
        public double debtEquityRatio { get; set; }
        public double debtEquityRatioWeighting { get; set; }
        public double interestCover { get; set; }
        public double interestCoverWeighting { get; set; }
        public double payoutRatio { get; set; }
        public double payoutRatioWeighting { get; set; }
        public double earningsStability { get; set; }
        public double earningsStabilityWeighting { get; set; }
    }
}