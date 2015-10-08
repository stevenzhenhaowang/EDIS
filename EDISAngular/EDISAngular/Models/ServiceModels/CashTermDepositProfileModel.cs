using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels
{
    public class CashTermDepositProfileModel
    {
        public List<CashTermDepositProfileItem> data { get; set; }
    }
    public class CashTermDepositProfileItem
    {
        public string accountType { get; set; }
        public string institution { get; set; }
        public string currency { get; set; }
        public string accountName { get; set; }
        public int daysToMaturity { get; set; }
        public double faceValue { get; set; }
        public string interestFrequency { get; set; }
        public double accruedInterest { get; set; }
        public string bsb { get; set; }
        public string accountNumber { get; set; }
        public DateTime depositDate { get; set; }
        public double depositAmount { get; set; }
        public DateTime maturityDate { get; set; }
        public double maturityValue { get; set; }
        public string termOfRates { get; set; }
        public double interestRate { get; set; }
        public double annualInterest { get; set; }
        public double suitabilityToInvestorProfile { get; set; }
    }
}