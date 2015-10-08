using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class SectorialPortfolioModel
    {
        public List<SectorItem> data { get; set; }
        public double total { get; set; }
        public double percentage { get; set; }
    }

    public class SectorItem
    {
        public string sector { get; set; }
        public double value { get; set; }
        public double percentage { get; set; }
    }

}