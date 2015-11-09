using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.AggregateRoots {
    public class CashAccount {
        public string Id { get; set; }
        public string Bsb { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public CashAccountType CashAccountType { get; set; }

        public DateTime? MaturityDate { get; set; }
        public Frequency Frequency { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public int? TermsInMonths { get; set; }
        public double? InterestRate { get; set; }
        public double? AnnualInterest { get; set; }

        public double? FaceValue { get; set; }

    }
}
