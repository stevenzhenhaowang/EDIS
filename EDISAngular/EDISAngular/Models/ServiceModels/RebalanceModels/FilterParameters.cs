using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class FilterParametersClassification
    {
        public string classificationType { get; set; }
        public List<FilterItem> filters { get; set; }

    }

    public class FilterItem
    {
        public string groupName { get; set; }
        public string groupId { get; set; }
        public List<string> identityMetaKey { get; set; }
        public double currentWeighting { get; set; }
        public string groupType { get; set; }
    }


}