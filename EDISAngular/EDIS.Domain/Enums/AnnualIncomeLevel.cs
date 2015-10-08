using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace EDIS_DOMAIN.Enum.Enums
{
    public enum AnnualIncomeLevel
    {
        [Description("Less than 75000")]
        LessThan75000 = 1,
        [Description("Between 75000 to 100000")]
        Between75000to100000 = 2,
        [Description("Between 100000 to 150000")]
        Between100000to150000=3,
        [Description("Greater Than 150000")]
        GreaterThan150000=4,
        [Description("Greater Than 250000")]
        GreaterThan250000=5,
        [Description("Greater Than 350000")]
        GreaterThan350000=6,
        [Description("Greater Than 450000")]
        GreaterThan450000=7,
        [Description("Greater Than 500000")]
        GreaterThan500000=8,
        [Description("Greater Than 750000")]
        GreaterThan750000=9,
        [Description("Greater Than 1000000")]
        GreaterThan1000000=10,
        [Description("Any Amount")]
        AnyAmount=11
    }
}