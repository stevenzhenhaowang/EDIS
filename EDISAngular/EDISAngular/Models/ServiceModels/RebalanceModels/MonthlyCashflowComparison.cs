using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class MonthlyCashflowComparison
    {
        public List<MonthlyCashflowComparisonData> data { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }
    }


    public class MonthlyCashflowComparisonData
    {
        public string groupName { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }
        public List<MonthlyCashflowComparisonItem> items { get; set; }
    }


    public class MonthlyCashflowComparisonItem
    {
        public string itemName { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }
    }




}