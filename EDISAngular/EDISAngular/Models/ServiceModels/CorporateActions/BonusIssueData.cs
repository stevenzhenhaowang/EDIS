﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class BonusIssueData
    {
        public string actionName { get; set; }
        public string ticker { get; set; }
        public string status { get; set; }
        public string edisAccountNumber { get; set; }
        public string bonusIssueShareAmount { get; set; }
        public DateTime bonusDate { get; set; }    
    }
}