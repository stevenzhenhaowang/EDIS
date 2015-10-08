using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum ProfessionType
    {
        [Description("Financial Planner")]
        FinancialPlanner = 1,
        [Description("Accountant")]
        Accountant = 2,
        [Description("Investment Adviser")]
        InvestmentAdviser = 3,
        [Description("Banker / Mortgage Broker")]
        BankerMortgageBroker = 4,
        [Description("Insurance Broker")]
        InsuranceBroker = 5,
        [Description("Business Development Manager")]
        BusinessDevelopmentManager = 6,
        [Description("Others")]
        Others = 7
    }
}