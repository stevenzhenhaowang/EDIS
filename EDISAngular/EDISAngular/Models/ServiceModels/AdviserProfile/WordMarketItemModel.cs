using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class WordMarketItemModel
    {
        public string name { get; set; }
        public double value { get; set; }
        public double changedValue { get; set; }
        public double changedRatePercentage { get; set; }
    }
}