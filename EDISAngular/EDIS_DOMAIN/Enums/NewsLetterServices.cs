using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum NewsLetterServices
    {
        [Description("Daily Bulletin (Free)")]
        DailyBulletin=1,
        [Description("Weekly Bulletin (Free)")]
        WeeklyBulletin=2,
        [Description("Monthly Bulletin (Free)")]
        MonthlyBUlletin=3,
        [Description("Initial Public Offers (IPOs)  (Free)")]
        InitialPublicOffers=4,
        [Description("Economic Seminars.  (Free if available)")]
        EconomicSeminars=5,
        [Description("Special EDIS Presentations. (Free if available)")]
        SpecialEdisPresentations=6,
        [Description("Information from CompareAdvisers.com")]
        InformationFromCompareAdvisers=7
    }
}