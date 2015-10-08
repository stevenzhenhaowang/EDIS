using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class SummaryGeneralInfoFCL
    {
        public double cost { get; set; }
        public double marketValue { get; set; }
        public double pl { get; set; }
        public double plp { get; set; }
        public double faceValue { get; set; }
        public double aveCoupon { get; set; }
    }
}