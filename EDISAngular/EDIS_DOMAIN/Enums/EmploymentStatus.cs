using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace EDIS_DOMAIN.Enum.Enums
{
    public enum EmploymentStatus
    {
        [Description("Employed - Full Time")]
        FullTime = 1,
        [Description("Employed - Part Time")]
        PartTime = 2,
        [Description("Employed - Casual")]
        Casual = 3,
        [Description("Unemployed")]
        Unemployed = 4,
        [Description("Retired")]
        Retired = 5,
        [Description("Semi Retired")]
        SemiRetired = 6,
        [Description("SelfEmployed")]
        SelfEmployed = 7,
        [Description("Pensioner")]
        Pensioner = 8
    }
}