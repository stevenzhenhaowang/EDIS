using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingSuitability
    {
        public double total { get; set; }
        public List<MarginLendingSuitabilityGroup> groups { get; set; }
    }


    public class MarginLendingSuitabilityGroup
    {
        public string type { get; set; }
        public double percentage { get; set; }
        public List<MarginLendingSuitabilityGroupItem> items { get; set; }
    }

    public class MarginLendingSuitabilityGroupItem
    {
        public string name { get; set; }
        public double percentage { get; set; }
    }


}