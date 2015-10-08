using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingTypeOfGearingModel
    {
        public double totalInvestmentIncome { get; set; }
        public double totalInvestmentExpenses { get; set; }
        public string netInvestmentPosition { get; set; }
        public string gearingStatus { get; set; }
    }
}