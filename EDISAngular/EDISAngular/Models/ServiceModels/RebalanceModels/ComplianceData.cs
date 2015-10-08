using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class ComplianceData
    {
        public string groupName { get; set; }
        public List<ComplianceDataItem> items { get; set; }
    }

    public class ComplianceDataItem
    {
        public string name { get; set; }
        public string apl { get; set; }
        public double suitability { get; set; }
        public string compliant { get; set; }
    }



}