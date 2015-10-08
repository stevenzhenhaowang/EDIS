using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class ParameterItemModel
    {
        public List<string> groupId { get; set; }
        public string itemName { get; set; }
        public string id { get; set; }
        public double marketValue { get; set; }
        public double currentValue { get; set; }
        public double currentWeighting { get; set; }
        public List<string> identityMetaKey { get; set; } 
    }
}