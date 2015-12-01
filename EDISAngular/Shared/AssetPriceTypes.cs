using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public enum AssetPriceTypes {
        [Description("Australian Equity")]
        AustralianEquity = 1,
        [Description("International Equity")]
        InternationalEquity = 2,
        [Description("Managed Investments")]
        ManagedInvestments = 3,
        [Description("Direct And Listed Property")]
        DirectAndListedProperty = 4,
        [Description("Fixed Income Investments")]
        FixedIncomeInvestments = 6
    }
}
