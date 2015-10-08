using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class IncomeDiversificationModel
    {
        public List<IncomeDiversificationItem> data { get; set; }
        public IncomeDiversificationItem total { get; set; }
    }

    public class IncomeDiversificationItem
    {
        public string name { get; set; }
        public double value { get; set; }
        public double percentage { get; set; }
    }

}