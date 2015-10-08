using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace EDIS_DOMAIN.Enum.Enums
{
    public enum ProfessionalAssociations
    {
        [Description("Financial Planning Association of Australia (FPA)")]
        FPA=1,
        [Description("Association of Financial Advisers (AFA)")]
        AFA=2,
        [Description("Certified Financial Planners (CFP)")]
        CFP=3,
        [Description("Association of Independently Owned Financial Professionals (AIOFP)")]
        AIOFP=4,
        [Description("CPA Australia (CPA)")]
        CPA=5,
        [Description("Institute of Chartered Accountants of Australia (CAA)")]
        CAA=6,
        [Description("Institute of Public Accountants (IPA)")]
        IPA=7,
        [Description("Associations of Accounting Technicians (AAT)")]
        AAT=8,
        [Description("Australian Bankers Association (ABA)")]
        ABA=9,
        [Description("Mortgage and Finance Association of Australia (MFAA)")]
        MFAA=10,
        [Description("Financial Services Institute of Australasia (Finsia)")]
        Finsia=11,
        [Description("Stockbrokers Association of Australia (SAA)")]
        SAA=12,
        [Description("National Insurance Brokers Association of Australia (NIBA)")]
        NIBA=13,
        [Description("Certified Insurance Professional (CIP)")]
        CIP=14,
        [Description("Qualified Practicing Insurance Broker (QPIB)")]
        QPIB=15
    }
}