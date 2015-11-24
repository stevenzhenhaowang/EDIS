using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class RightsIssueData
    {
        public string actionName { get; set; }
        public string ticker { get; set; }
        public DateTime RightsIssueDate { get; set; }
        public string cashAdjustment { get; set; }
        public string shareAdjustment { get; set; }
        public string status { get; set; }
        public string edisAccountNumber { get; set; }
       
    }
}