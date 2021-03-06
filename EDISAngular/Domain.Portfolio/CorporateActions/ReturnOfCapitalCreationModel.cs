﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class ReturnOfCapitalCreationModel
    {
        public string ActionName { get; set; }
        public string Ticker { get; set; }
        public string AdviserId { get; set; }
        public DateTime? AdjustmentDate { get; set; }
        public List<ReturnOfCapitalParticipantAccounts> AccountsInfo { get; set; }
    }
    public class ReturnOfCapitalParticipantAccounts {
        public string AccountNumber { get; set; }
        public string ReturnAmount { get; set; }
    }
}
