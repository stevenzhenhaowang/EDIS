using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class GeoGraphicalLocations
    {
        public string id { get; set; }
        public string State { get; set; }
        public int Clients { get; set; }
        public List<GeoCityData> Data { get; set; }
    }

    public class GeoCityData
    {
        public string id { get; set; }
        public string City { get; set; }
        public int Clients { get; set; }
        public List<GeoSuburbData> Data { get; set; }
    }

    public class GeoSuburbData
    {
        public string id { get; set; }
        public string Suburb { get; set; }
        public int Clients { get; set; }
    }

   

}