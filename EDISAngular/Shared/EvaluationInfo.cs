
using System.ComponentModel;

namespace Shared {
    public enum EvaluationInfo {

        [Description("One Year Return")]
        OneYearReturn = 1,
        [Description("Five Year Return")]
        FiveYearReturn = 2,
        [Description("Debt Equity Ratio")]
        DebtEquityRatio = 3,
        [Description("Eps Growth")]
        EpsGrowth = 4,
        [Description("Dividend Yield")]
        DividendYield = 5,
        [Description("Franking")]
        Frank = 6,
        [Description("Interest Cover")]
        InterestCover = 7,
        [Description("Price Earning Ratio")]
        PriceEarningRatio = 8,
        [Description("Return On Asset")]
        ReturnOnAsset = 9,
        [Description("Return On Equity")]
        ReturnOnEquity = 10
    }
}
