using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace EDIS_DOMAIN.Enum.Enums
{
    public enum InvestmentKnowledge
    {
        [Description("You have little or no experience.")]
        LittleOrNo=1,
        [Description("You have basic understanding of investment markets.")]
        BasicUnderstanding=2,
        [Description("You have a strong understanding of investment markets.")]
        StrongUnderstanding=3,
        [Description("You are an experience investor.")]
        ExperienceInvestor=4
    }
}