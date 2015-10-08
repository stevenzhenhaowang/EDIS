using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingDiversificationModel
    {
        public double totalNumberOfSecurities { get; set; }
        public double numberOfSecurities90lvr { get; set; }
        public double numberOfSecurities80lvr { get; set; }
        public double numberOfSecurities70lvr { get; set; }
        public double numberOfSecurities60lvr { get; set; }
        public double numberOfSecurities50lvr { get; set; }
        public double numberOfSecurities40less { get; set; }
    }
}