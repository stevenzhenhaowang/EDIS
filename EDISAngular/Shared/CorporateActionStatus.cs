using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum CorporateActionStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Rejected")]
        Rejected = 3,
        [Description("Mandatory")]
        Mandatory =4,

    }
}
