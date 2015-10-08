using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;



namespace EDIS_DOMAIN.Enum.Enums
{
    public enum PictureApprovalStatus
    {
        [Description("Pending Approval")]
        PendingApproval=1,
        [Description("Approved")]
        Approved=2,
        [Description("Rejected")]
        Rejected=3
    }
}