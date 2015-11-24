using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class BuyBackProgramData
    {
        public string actionName { get; set; }
        public string ticker { get; set; }
        public DateTime buyBackDate { get; set; }
        public string status { get; set; }
        public string edisAccountNumber { get; set; }
        public string shareAmountAdjustment { get; set; }
        public string cashAdjusment { get; set; }
    }
}