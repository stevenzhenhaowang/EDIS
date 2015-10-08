using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{

    public class PortfolioStasticsModel
    {
        public List<PortfolioStasticsItem> data { get; set; }
        public double totalCostInvestment { get; set; }
        public double totalCostWeighting { get; set; }
        public double totalMarketValue { get; set; }
        public double totalMarketValueWeighting { get; set; }
        public double totalpl { get; set; }
    }


    public class PortfolioStasticsItem
    {
        public string assetClass { get; set; }
        public double costInvestment { get; set; }
        public double costWeighting { get; set; }
        public double marketValue { get; set; }
        public double marketWeighting { get; set; }
        public double pl { get; set; }
        public double plp { get; set; }
        public double suitability { get; set; }
        public double oneYearReturn { get; set; }
        public double threeYearReturn { get; set; }
        public double fiveYearReturn { get; set; }
        public double earningsPerShare { get; set; }
        public double dividends { get; set; }
        public double dividend { get; set; }
        public string beta { get; set; }
        public double returnOnAsset { get; set; }
        public double returnOnEquity { get; set; }
        public double priceEarningsRatio { get; set; }
        public string priceBook { get; set; }
        public string priceCashflow { get; set; }
        public string avMarketCap { get; set; }
    }
}