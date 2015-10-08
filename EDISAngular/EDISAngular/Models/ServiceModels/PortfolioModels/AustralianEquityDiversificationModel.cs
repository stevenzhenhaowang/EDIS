using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class EquityDiversificationModel
    {
        public EquityDiversificationTotalModel total { get; set; }
        public List<EquityDiversificationDataItem> data { get; set; }
    }


    public class EquityDiversificationTotalModel
    {
        public double sectorialExposureValue { get; set; }
        public double sectorialExposurePercentage { get; set; }
        public double asxSectorialExposurePercentage { get; set; }
        public double asxDifferencesPercentage { get; set; }
        public double profileAllocationPercentage { get; set; }
        public double actualPercentage { get; set; }
        public double profileDifferencesPercentage { get; set; }
        public string indicator { get; set; }
        public double suitabilityIndicator { get; set; }
    }

    public class EquityDiversificationDataItem
    {
        public string sector { get; set; }
        public double sectorialExposureValue { get; set; }
        public double sectorialExposurePercentage { get; set; }
        public double asxSectorialExposurePercentage { get; set; }
        public double asxDifferencesPercentage { get; set; }
        public double profileAllocationPercentage { get; set; }
        public double actualPercentage { get; set; }
        public double profileDifferencesPercentage { get; set; }
        public string indicator { get; set; }
        public double suitabilityIndicator { get; set; }
    }


}