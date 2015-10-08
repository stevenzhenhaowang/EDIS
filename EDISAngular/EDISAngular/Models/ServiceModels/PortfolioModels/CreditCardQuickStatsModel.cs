using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class CreditCardQuickStatsModel
    {
        public double totalOutstandingShortTermDebt { get; set; }
        public double totalAvailableShortTermCredit { get; set; }
        public double totalAvailableFunds { get; set; }
    }
}