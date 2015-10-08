using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class GeoStatsModel
    {
        public GeoGroupItem ClientByAge { get; set; }
        public GeoGroupItem AssetsUnderManagement { get; set; }
        public GeoGroupItem ClientProfileClassification { get; set; }
        public GeoGroupItem ClientTimeFrameClassification { get; set; }
        public GeoRankingItem TopUsedInvestments { get; set; }
        public GeoRankingItem TopUsedDebt { get; set; }



    }

    public class GeoGroupItem
    {
        public int Total { get; set; }
        public List<GeoGroupDataItem> Data { get; set; }
    }

    public class GeoRankingItem
    {
        public double Total { get; set; }
        public List<GeoRankingDataItem> Data { get; set; }
    }

    public class GeoRankingDataItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class GeoGroupDataItem
    {
        public string Group { get; set; }
        public int Number { get; set; }
    }
}