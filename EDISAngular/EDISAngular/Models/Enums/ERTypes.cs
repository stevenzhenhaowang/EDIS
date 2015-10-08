using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace EDISAngular.Models.Enums
{
    public enum ERTypes
    {
        [Description("Adviser Service Fee")]
        AdviserServicesFee=1,
        [Description("Transaction Fee (Third Party Fee)")]
        TransactionFee=2,
        [Description("Other Fee")]
        OtherFee=3,
        [Description("Dividend")]
        Dividend=4,
        [Description("Interest")]
        Interest=5,
        [Description("Other Income")]
        OtherIncome=6
    }
}