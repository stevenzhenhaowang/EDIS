﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class ReinvestmentData
    {
        public string actionName { get; set; }
        public string accountNumber { get; set; }
        public string ticker { get; set; }
        public string reinvestmentShareAmount { get; set; }
        public DateTime reinvestmentDate { get; set; }
        public string status { get; set; }
        //public List<ReinvestmentParticipant> participants { get; set; }
    }

    public class ReinvestmentParticipant {

        public string edisAccountNumber { get; set; }
    }
}