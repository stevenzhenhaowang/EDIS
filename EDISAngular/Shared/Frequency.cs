using System.ComponentModel;
namespace Shared
{
    public enum Frequency
    {
        [Description("Daily")]
        Daily = 1,
        [Description("Weekly")]
        Weekly = 2,
        [Description("Monthly")]
        Monthly = 3,
        [Description("Quarterly")]
        Quarterly = 4,
        [Description("Semiannually")]
        Semiannually = 5,
        [Description("Annually")]
        Annually = 6
    }
}