using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingCashflowModel
    {
        public double totalMonthlyCashflow { get; set; }
        public double monthlyMarginLoanExpenses { get; set; }
        public double netMonthlyCashflow { get; set; }
        public string situationStatus { get; set; }


    }
}