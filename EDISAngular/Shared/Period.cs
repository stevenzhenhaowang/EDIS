
using System.ComponentModel;

namespace Shared
{
    public enum Period
    {
        [Description("Last month")]
        LastMonth = 1,
        [Description("Last 6 months")]
        LastSixMonths = 2,
        [Description("Last 12 months")]
        LastTwelveMonths = 3,
        [Description("Last 3 years")]
        LastThreeYears = 4,
        [Description("All")]
        All = 5,


    }
}
