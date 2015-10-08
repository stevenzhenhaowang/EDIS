using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class AssetPerformanceModel
    {
        public string topic { get; set; }
        public string asset { get; set; }
        public double value { get; set; }
        public double rturn { get; set; }
        public double weighting { get; set; }
    }


}