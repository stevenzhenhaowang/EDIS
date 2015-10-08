using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class SectorialComparisonModel
    {
        public List<SectorialComparisonData> data { get; set; }
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }

    }


    public class SectorialComparisonData
    {
        public string groupName { get; set; }
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }
        public List<SectorialComparisonItem> items { get; set; }
    }

    public class SectorialComparisonItem
    {
        public string itemName { get; set; }
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }
    }






}