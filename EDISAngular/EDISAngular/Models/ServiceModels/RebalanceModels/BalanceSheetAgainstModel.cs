using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class BalanceSheetAgainstModel
    {
        public List<BalanceSheetAgainstModelData> data { get; set; }
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }

    }


    public class BalanceSheetAgainstModelData 
    {
        public string groupName { get; set; }//asset/liability?
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }
        public List<BalanceSheetAgainstModelItem> items { get; set; }
    }


    public class BalanceSheetAgainstModelItem
    {
        public string itemName { get; set; }//asset type name: Australian equity, International equity, etc.
        public double current { get; set; }
        public double currentWeighting { get; set; }
        public double proposed { get; set; }
        public double proposedWeighting { get; set; }
        public double difference { get; set; }

    }




}