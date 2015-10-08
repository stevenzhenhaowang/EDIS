using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum DeclineCommunicationReasons
    {
        [Description("My apologies, I am unable to accept your communications due to capacity reasons. I wish you all the best.")]
        CapacityReasons = 1,
        [Description("My apologies, your profile do match my client demographics that I manage.  I wish you all the best.")]
        DemographicsNotMatch = 2,
        [Description("My apologies, your request is outside of my licensing capability. I wish you all the best.")]
        OutsideOfLicensingCapability = 3,
        [Description("My apologies, I am transitioning to a new employment opportunity, please try me again after 2 weeks.")]
        TransitioningToNewEmploymentOpportunity = 4,
        [Description("My apologies, my role has changed and I cannot service you in the same capacity.  I wish you all the best.")]
        RoleChangedAndOutOfCapacity = 5,
        [Description("My apologies, I cannot service you at present.  I wish you all the best.")]
        CannotService = 6,
        [Description("Other")]
        Other = 7
    }
}