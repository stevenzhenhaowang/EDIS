using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingCompanyProfileModel
    {
        public string ticker { get; set; }
        public string company { get; set; }
        public int units { get; set; }
        public double costPrice { get; set; }
        public double brokerage { get; set; }
        public double netCost { get; set; }
        public double marketPrice { get; set; }
        public double marketValue { get; set; }
        public double plPercentage { get; set; }
        public double plValue { get; set; }
        public double annualIncome { get; set; }
        public double annualInterestExpenses { get; set; }
        public string cashflowPosition { get; set; }
        public string gearing { get; set; }
        public int suitability { get; set; }

    }
}