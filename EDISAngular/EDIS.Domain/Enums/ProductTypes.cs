using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum ProductTypes
    {
        [Description("Australian Equity")]
        AustralianEquity = 1,
        [Description("Cash investments")]
        CashInvestments = 2,
        [Description("Direct Commodity")]
        DirectCommodity = 3,
        [Description("Exchanged Traded Fund")]
        ExchangedTradedFund = 4,
        [Description("Financial Planner")]
        FinancialPlanner = 5,
        [Description("Fixed Income Investments")]
        FixedIncomeInvestments = 6,
        [Description("Hybrid Securities")]
        HybridSecurities = 7,
        [Description("Initial Public Offer")]
        InitialPublicOffer = 8,
        [Description("International Equity")]
        InternationalEquity = 9,
        [Description("Investment Adviser")]
        InvestmentAdviser = 10,
        [Description("Investment Loan")]
        InvestmentLoan = 11,
        [Description("Managed Investments")]
        ManagedInvestments = 12,
        [Description("Margin Lending")]
        MarginLending = 13,
        [Description("Model Portfolio")]
        ModelPortfolio = 14,
        [Description("Money Market Investment")]
        MoneyMarketInvestment = 15,
        [Description("Online Trading")]
        OnlineTrading = 16,
        [Description("Options")]
        Options = 17,
        [Description("Regular Gearing")]
        RegularGearing = 18,
        [Description("Unlisted Investment Property")]
        UnlistedInvestmentProperty = 19,
        [Description("Warrants")]
        Warrants = 20,
        [Description("Wrap Platforms")]
        WrapPlatforms = 21
    }
}
