using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class MonthlyInvestmentIncomeModel
    {
        public double current { get; set; }
        public double proposed{ get; set; }
        public double difference { get; set; }
        public List<MonthlyInvestmentIncomeData> data { get; set; }
    }

    public class MonthlyInvestmentIncomeData
    {
        public string name { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }

    }
}