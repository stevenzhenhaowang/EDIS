using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace EDIS_DOMAIN.Enum.Enums
{
    public enum BalanceGroups
    {
        [Description("Australian Equity")]
        AustralianEquity = 1,
        [Description("International Equity")]
        InternationalEquity = 2,
        [Description("Managed Investments")]
        ManagedInvestments = 3,
        [Description("Direct And Listed Property")]
        DirectAndListedProperty = 4,
        [Description("Miscellaneous Investments")]
        MiscellaneousInvestments = 5,
        [Description("Fixed Income Investments")]
        FixedIncomeInvestments = 6,
        [Description("Cash and Term Deposit")]
        CashAndTermDeposit = 7,


        #region loan types temp
        [Description("Mortgage and Home Loan")]
        MortgageAndHomeLoan = 8,
        [Description("Margin Lending")]
        MarginLending = 9,
        [Description("Personal & Credit Card Loan")]
        PersonalAndCreditCardLoan = 10,
        [Description("Insurance")]
        Insurance = 11
        #endregion

    }
}