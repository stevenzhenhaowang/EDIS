using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CashTermDepositGeneralInfoModel
    {
        public double cashDeposit { get; set; }
        public double cashAtMaturity { get; set; }
        public double annualInterest { get; set; }
        public double annualYield { get; set; }
        public double rbaRate { get; set; }
        public double consumerPriceIndex { get; set; }
    }
}