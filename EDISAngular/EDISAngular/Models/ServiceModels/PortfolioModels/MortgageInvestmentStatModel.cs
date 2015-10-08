using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MortgageInvestmentStatModel
    {
        //public int debtLevel { get; set; }
        //public double incomeLevel { get; set; }
        //public double cashfFlowLevel { get; set; }
        //public double marketValue { get; set; }
        //public double repayments { get; set; }

        public double NotSpecified { get; set; }
        public double Fixed { get; set; }
        public double Variable { get; set; }
        public double Combination { get; set; }
    }
}