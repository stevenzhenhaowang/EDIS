
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CreditCardDetailsModel
    {
        public string type { get; set; }
        public string issuer { get; set; }
        public string assetSecurity { get; set; }
        public double maximumCreditLimit { get; set; }
        public double accountBalance { get; set; }
        public double availableFunds { get; set; }
        public double termOfLoan { get; set; }
        public DateTime expiryDate { get; set; }
        public double interestRate { get; set; }
    }
}