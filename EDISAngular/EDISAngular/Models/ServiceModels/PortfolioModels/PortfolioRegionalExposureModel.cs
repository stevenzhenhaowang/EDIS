using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class PortfolioRegionalExposureModel
    {
        public List<RegionModel> data { get; set; }
        public double total { get; set; }
    }


    public class RegionModel
    {
        public string name { get; set; }
        public double percentage { get; set; }
        public double totalAsset { get; set; }
        public List<RegionalAssetModel> assets { get; set; }
    }

    public class RegionalAssetModel
    {
        public string country { get; set; }
        public double assetValue { get; set; }
        public double assetWeighting { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }

}