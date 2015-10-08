using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace EDIS_DOMAIN.Enum.Enums
{
    public enum AttitudeToCapitalLoss
    {
        [Description("Redeeming all assets and transferring to cash")]
        RedeemingAllAssetsToCash=1,
        [Description("Be concerned and consider changing investment strategy")]
        ConcernedAndChangeStrategy=2,
        [Description("Comfortable with existing strategy if income is maintained")]
        ComfortableIfIncomeMaintained=3,
        [Description("Investing more to reduce dollar cost average of investment")]
        InvestMoreToReduceCostAverage=4
    }
}