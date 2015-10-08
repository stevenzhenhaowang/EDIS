using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class AnticipatedCostModel
    {
        public List<AnticipatedCostData> data { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }
    }

    public class AnticipatedCostData
    {
        public string name { get; set; }
        public double current { get; set; }
        public double proposed { get; set; }
        public double difference { get; set; }
    }


}