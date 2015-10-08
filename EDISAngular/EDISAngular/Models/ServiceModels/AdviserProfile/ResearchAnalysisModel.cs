using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class CompanyProfileModel
    {
        public CompanyProfileMetaData metaData { get; set; }

        public List<CompanyProfileDataItem> data { get; set; }

    }

    public class CompanyProfileDataItem
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string ticker { get; set; }
        public string country { get; set; }
        public string exchange { get; set; }
        public string assetClass { get; set; }
        public string sector { get; set; }
        public double closingPrice { get; set; }
        public double changeAmount { get; set; }
        public double changeRatePercentage { get; set; }
        public double weeksDifferenceAmount { get; set; }
        public double weeksDifferenceRatePercentage { get; set; }
        public string marketCapitalisation { get; set; }
        public DateTime priceDate { get; set; }
        public string currencyType { get; set; }
        public double suitabilityScore { get; set; }
        public string suitsTypeOfClients { get; set; }
        public string reasons { get; set; }
        public string companyBriefing { get; set; }
        public string companyStrategies { get; set; }
        public string investment { get; set; }
        public string investmentName { get; set; }
        public CurrentAnalysisPayload currentAnalysis { get; set; }
        public ForecastAnalysisPayload forecast { get; set; }
        public ComparisonDetailsModel comparisonDetails { get; set; }
        public List<CompanyProfileIndexData> indexData { get; set; }

    }

    public class CompanyProfileIndexData
    {
        public DateTime date { get; set; }
        public double company { get; set; }
        public double portfolio { get; set; }
        public double asx200 { get; set; }
        public string month { get; set; }
    }



    public class CurrentAnalysisPayload
    {
        public List<AnalysisPayloadMetaProperty> metaProperties { get; set; }
        public List<AnalysisPayloadGroupModel> groups { get; set; }
        
    }

    public class ForecastAnalysisPayload
    {
        public List<AnalysisPayloadMetaProperty> metaProperties { get; set; }
        public List<AnalysisPayloadGroupModel> oneYearForecastGroups { get; set; }
        public List<AnalysisPayloadGroupModel> twoYearForecastGroups { get; set; }
    }



    public class ComparisonDetailsModel
    {
        public ComparisonDetailsSuitability suitability { get; set; }
        public ComparisonDetailsRecommendationSuitability recommendationSuitability { get; set; }
        public ComparisonDetailsInvestmentAnalysis investmentAnalysis { get; set; }
    }

    public class ComparisonDetailsSuitability
    {
        public string annualReturn { get; set; }
        public string oneYearBeta { get; set; }
        public string companyGearing { get; set; }
        public string debtEquityRatio { get; set; }
        public string dividendYield { get; set; }
        public string earningsPerShareGrowth { get; set; }
        public string interestCover { get; set; }
        public string marketCapitalisation { get; set; }
        public string priceEarningsRatio { get; set; }
        public double totalStandardRating { get; set; }
    }
    public class ComparisonDetailsRecommendationSuitability
    {
        public string recommendationOnInvestmentAsset { get; set; }
        public string forecastDividendYield { get; set; }
        public string forecastDividendFranking { get; set; }
        public string forecastEarningsPerShareGrowth { get; set; }
        public string forecastOneYearBeta { get; set; }
        public string forecastCompanyGearing { get; set; }
        public string forecastPriceEarningsRatio { get; set; }
        public string forecastPriceEarningsRatioGrowth { get; set; }
        public string forecastInterestCover { get; set; }
        public string forecastDebtEquityRatio { get; set; }
        public string forecastReturnOnEquity { get; set; }
        public string forecastReturnOnAsset { get; set; }
        public string forecastAssetAllocation { get; set; }
        public double totalRecommendationSuitabilityScore { get; set; }
    }
    public class ComparisonDetailsInvestmentAnalysis
    {
        public ComparisonDetailsInvestmentGeneralInfo generalInformation { get; set; }
        public ComparisonDetailsInvestmentRecommendation recommendation { get; set; }
    }

    public class ComparisonDetailsInvestmentGeneralInfo
    {
        public ComparisonDetailsInvestmentAnalysisItem currentPrice { get; set; }
        public ComparisonDetailsInvestmentAnalysisItem priceChangeAmount { get; set; }
    }
    public class ComparisonDetailsInvestmentRecommendation
    {
        public ComparisonDetailsInvestmentAnalysisItem currentShortTermRecommendation { get; set; }
        public ComparisonDetailsInvestmentAnalysisItem currentLongTermRecommendation { get; set; }
    }



    public class ComparisonDetailsInvestmentAnalysisItem
    {
        public string baseInformation { get; set; }
        public string clientObjective { get; set; }
        public string clientSuitability { get; set; }
    }




    public class AnalysisPayloadMetaProperty
    {
        public string propertyName { get; set; }
        public string displayName { get; set; }
    }

    public class AnalysisPayloadGroupModel
    {
        public string name { get; set; }
        public List<AnalysisPayloadGroupDataItem> data { get; set; }
    }

    public class AnalysisPayloadGroupDataItem
    {
        public string name { get; set; }
        public string baseInformation { get; set; }
        public string morningstar { get; set; }
        public string brokerX { get; set; }
        public string ASX200Accumulation { get; set; }
    }






    public class CompanyProfileMetaData
    {
        public CompanyProfileInvestmentAnalysisProperties compareDetailsInvestmentAnalysisProperties { get; set; }
    }
    public class CompanyProfileInvestmentAnalysisProperties
    {
        public string propertyName { get; set; }
        public string displayName { get; set; }
        public List<CompanyProfileInvestmentAnalysisProperty> properties { get; set; }
    }
    public class CompanyProfileInvestmentAnalysisProperty
    {
        public string propertyName { get; set; }
        public string displayName { get; set; }
    }
    public class AnalysisCityBrief
    {
        public string name { get; set; }
        public string id { get; set; }
    }


}