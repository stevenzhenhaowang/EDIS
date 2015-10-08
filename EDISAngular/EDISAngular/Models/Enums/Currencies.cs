using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum Currencies
    {
        [Description("Australian Dollars")]
        AUD=1,
        [Description("US Dollars")]
        USD=2
    }
}