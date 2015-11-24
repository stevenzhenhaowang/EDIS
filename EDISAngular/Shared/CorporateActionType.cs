using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum CorporateActionType
    {
        [Description("Return Of Capital")]
        ReturnOfCapital = 1,
        [Description("Reinvestment Plan")]
        ReinvestmentPlan = 2,
        [Description("Stock Split")]
        StockSplit = 3,
        [Description("Bonus Issue")]
        BonusIssue =4,
        [Description("Buy Back Program")]
        BuyBackProgram = 5,
        [Description("Rights Issue")]
        RightsIssue = 6,
    }
}
