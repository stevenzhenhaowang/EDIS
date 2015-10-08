using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum InvestibleAssetsLevel
    {
        [Description("Less than $100,000")]
        LessThan100000=1,
        [Description("Between $100,000 to $200,000")]
        Between100000to200000=2,
        [Description("Between $200,000 to $400,000")]
        Between200000to400000=3,
        [Description("Between $400,000 to $600,000")]
        Between400000to600000=4,
        [Description("Greater than $600,000")]
        GreaterThan60000=5,
        [Description("Any Amount")]
        AnyAmount=6
    }
}