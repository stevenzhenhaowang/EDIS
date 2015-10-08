using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{

    /// <summary>
    /// Represents the type of the parameters for rebalance model
    /// </summary>
    public class FilterGroupModel
    {
        public string classificationType { get; set; }
        public List<FilterGroupFilter> filters { get; set; }
    }



    public class FilterGroupFilter
    {
        public string groupName { get; set; }
        public string groupId { get; set; }//used to retrieve relevant parameters
        public List<string> identityMetaKey { get; set; }
        public double currentWeighting { get; set; }
        public string groupType { get; set; }
    }
}