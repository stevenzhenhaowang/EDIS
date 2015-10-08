using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum SubscriptionPlanTypes
    {
        [Description("Free 3 Referrals")]
        Free3Referrals=1,
        [Description("Free Subscription 12 months - 3 Referrals")]
        Free12Month3R=2,
        [Description("Free Subscription 12 months - 5 Referrals")]
        Free12Month5R=3,
        [Description("Free Subscription 12 months - Unlimited Referrals")]
        Free12MonthUnlimited=4,
        [Description("Full Subscription 12 months")]
        Full12Month=5
    }
}