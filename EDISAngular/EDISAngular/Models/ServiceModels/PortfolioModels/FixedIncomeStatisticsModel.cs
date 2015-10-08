using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class IncomeStatisticsModel
    {
        public string type { get; set; }
        public double value { get; set; }
        public double percentage { get; set; }
    }
}