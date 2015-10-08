using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDISAngular.Models.Enums
{
    public enum BalanceClass
    {
        [Description("Asset")]
        Asset=1,
        [Description("Liability")]
        Liability=2
    }
}