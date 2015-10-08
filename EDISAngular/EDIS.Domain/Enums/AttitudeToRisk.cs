using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum AttitudeToRisk
    {
        [Description("Prefer lower growth to avoid volatile returns")]
        PreferLowerGrowth=1,
        [Description("Achieve steady growth and accept some volatility of returns")]
        AchieveSteadyGrowthAcceptVolatility=2,
        [Description("Take on higher volatility for greater returns")]
        HigherVolatilityHighReturn=3,
        [Description("Willing to risk all or majority of portfolio in assets with higher levels of capital fluctuation")]
        takeMajorRisks=4
    }
}