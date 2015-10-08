using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;




namespace EDIS_DOMAIN.Enum.Enums
{
    public enum InvestedProducts
    {
        [Description("Australian Equity")]
        AustralianEquity=1,
        [Description("International Equity")]
        InternationalEquity=2,
        [Description("Exchange Traded Funds")]
        ExchangeTradeFunds=3,
        [Description("Derivatives - Exchange Traded Options")]
        DerivativesExhcnageTradedOptions=4,
        [Description("Derivatives - Warrants")]
        DerivativesWarrants=5,
        [Description("Hybrid Securities")]
        HybridSecurities=6,
        [Description("Managed Investments")]
        ManagedInvestments=7,
        [Description("Fixed Income Investments")]
        FixedIncomeInvestments=8,
        [Description("Direct Property Investments")]
        DirectPropertyInvestments=9,
        [Description("Initial Public Offers (IPO)")]
        InitialPublicOffers=10,
        [Description("Cash and Term Deposit Investments")]
        CashAndTermDepositInvestments=11,
        [Description("Structured and Unlisted Investment Products")]
        StructuredAndUnlistedInvestmentProducts=12,
        [Description("Margin Lending")]
        MarginLending=13,
        [Description("Contract for Difference (CFD)")]
        ContractForDifference=14,
        [Description("Agriculture Schemes")]
        AgricultureSchemes=15,
        [Description("Personal Home Loan")]
        PersonalHomeLoan=16,
        [Description("Investment Home Loan")]
        InvetmentHomeLoan=17,
        [Description("Personal Loan")]
        PersonalLoan=18,
        [Description("Line of Credit")]
        LineOfCredit=19,
        [Description("Commercial Loan")]
        CommercialLoan=20,
        [Description("Asset Finance")]
        AssetFinance=21,
        [Description("General Insurance")]
        GeneralInsurance=22,
        [Description("Health Insurance")]
        HealthInsurance=23,
        [Description("Life Insurance")]
        LifeInsurance=24,
        [Description("Income Protection Insurance")]
        IncomeProtectionInsurance=25,
        [Description("Motor Vehicle Insurance")]
        MotorVehicleInsurance=26,
        [Description("Professional indemnity Insurance")]
        ProfessionalIndemnityInsurance=27,
        [Description("Business Insurance")]
        BusinessInsurance=28,
        [Description("Self-Managed Superannuation Fund")]
        SelfManagedSuperannuationFund=29,
        [Description("Trust & Company")]
        TrustAndCompany=30





    }
}