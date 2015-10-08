using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum TotalAssetLevels
    {
        [Description("Less than $500,000")]
        LessThan500000 = 1,
        [Description("Between $500,000 to $750,000")]
        Between500000to750000 = 2,
        [Description("Between $750,000 to $1,000,000")]
        Between750000to1000000 = 3,
        [Description("Between $1,000,000 to $1,500,000")]
        Between1000000to1500000 = 4,
        [Description("Greater than $1,500,000")]
        GreaterThan1500000 = 5,
        [Description("Any Amount")]
        AnyAmount = 6
    }
}