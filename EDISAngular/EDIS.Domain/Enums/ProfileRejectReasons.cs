using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum ProfileRejectReasons
    {
        [Description("the profile commentary is inappropriate, please supply another suitable brief.")]
        CommentaryInappropriate=1,
        [Description("the profile commentary is unprofessional, please supply another suitable brief.")]
        CommentaryUnprofessional=2,
        [Description("the profile commentary contains your company details and/or contact details, please remove it. This is for your own protection in case of harassment and inappropriate actions by non-genuine clients. This information will be supplied to the potential client, when approved by you with the contact request.")]
        ContainsSensitiveData=3,
        [Description("the profile commentary contains potential misleading and deceptive statements. Please rephrase your wording and do not guarantee anything.")]
        ContainsMisleadingData=4
    }
}