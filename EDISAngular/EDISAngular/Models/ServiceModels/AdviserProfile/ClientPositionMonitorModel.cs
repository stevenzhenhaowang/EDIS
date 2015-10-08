using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class ClientPositionMonitorModel
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AssetClass { get; set; }
        public double NetCost { get; set; }
        public double MarketValue { get; set; }
        public double PL { get; set; }
        public double PLRate { get; set; }
        public string CompliantStatus { get; set; }
        public int Reminders { get; set; }
        public List<ClientPositionMonitorType> Type { get; set; }



    }

    public class ClientPositionMonitorType
    {
        public string Name { get; set; }
        public List<ClientPositionMonitorTypeData> Data { get; set; }
    }


    public class ClientPositionMonitorTypeData
    {
        public string Name { get; set; }
        public double NetCost { get; set; }
        public double MarketValue { get; set; }
        public double PL { get; set; }
        public double PLRate { get; set; }
        public string CompliantStatus { get; set; }
        public int Reminders { get; set; }
    }


}