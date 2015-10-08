using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class DirectPropertyGeoModel
    {
        public List<DirectPropertyGeoItem> data { get; set; }
    }

    public class DirectPropertyGeoItem
    {
        public string id { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string type { get; set; }
        public double value { get; set; }
    }


}