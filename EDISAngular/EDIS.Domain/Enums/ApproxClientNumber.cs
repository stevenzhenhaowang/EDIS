using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum ApproxClientNumber
    {
        [Description("From 0 to 100 Clients")]
        From0to100Clients = 1,
        [Description("From 101 to 200 Clients")]
        From101to200Clients = 2,
        [Description("From 201 to 400 Clients")]
        From201to400Clients = 3,
        [Description("From 401 to 600 Clients")]
        From401to600Clients = 4,
        [Description("From 601 to 1000 Clients")]
        From601to1000Clients = 5,
        [Description("From 1001 to 1500 Clients")]
        From1001to1500Clients = 6,
        [Description("From 1501 or Greater Clients")]
        From1501orGreaterClients = 7


    }
}