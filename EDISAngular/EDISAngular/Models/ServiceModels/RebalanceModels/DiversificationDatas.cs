using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class DiversificationDatas
    {
        public double portfolioWeighting { get; set; }
        public double modelWeighting { get; set; }
        public string group { get; set; }//asset type name.
    }
}