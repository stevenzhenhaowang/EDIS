using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class DiversificationGroupModel
    {
        public double total { get; set; }
        public List<DiversificationGroupItem> data { get; set; }
    }

    public class DiversificationGroupItem
    {
        public string group { get; set; }
        public List<DiversificationGroupData> data { get; set; }
    }

    public class DiversificationGroupData
    {
        public string title { get; set; }
        public double value { get; set; }
    }


}