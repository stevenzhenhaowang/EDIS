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
        [Description("ReturnOfCapital")]
        ReturnOfCapital = 1,
        [Description("ReinvestmentPlan")]
        ReinvestmentPlan = 2,
        [Description("StockSplit")]
        StockSplit = 3,
        [Description("BonusIssue")]
        BonusIssue =4,
        [Description("BuyBackProgram")]
        BuyBackProgram = 5,
        [Description("RightsIssue")]
        RightsIssue = 6,
    }
}
