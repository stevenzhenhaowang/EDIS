using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum CommissionsAndFees
    {
        [Description("Receive Trail Commission on Investments")]
        ReceiveTrailCommissionOnInvestments = 1,
        [Description("Ongoing Service Fees")]
        OngoingServiceFees = 2,
        [Description("Cost for Statement of Advice")]
        CostForStatementOfAdvice = 3,
        [Description("Cost of Record of Advice")]
        CostOfRecordOfAdvice = 4,
        [Description("Cost of Implementation of Advice")]
        CostOfImplementationOfAdvice = 5,
        [Description("Other Charges")]
        OtherCharges = 6,
        [Description("Remuneration")]
        Remuneration = 7
    }
}