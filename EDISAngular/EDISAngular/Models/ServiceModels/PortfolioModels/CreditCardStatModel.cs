using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CreditCardStatModel
    {
        public string accountName { get; set; }
        public string bsb { get; set; }
        public string accountNumber { get; set; }
        public string repaymentFrequency { get; set; }
        public DateTime lastPaymentDate { get; set; }
        public double lastRepaymentAmount { get; set; }
        public DateTime nextRepaymentDate { get; set; }
        public double nextRepaymentAmount { get; set; }
        public double daysToLoanExpiry { get; set; }
        public string awardsMember { get; set; }
        public double currentAwardPoints { get; set; }
        public double purchasedInterestRate { get; set; }
        public double cashAdvanceRate { get; set; }



    }
}