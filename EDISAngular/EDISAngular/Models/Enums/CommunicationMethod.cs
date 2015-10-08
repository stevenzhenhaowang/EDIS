using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum CommunicationMethod
    {
        [Description("By Phone")]
        ByPhone = 1,
        [Description("By Mobile")]
        ByMobile = 2,
        [Description("By Email")]
        ByEmail = 3
    }
}