﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class CorperateActionParticipateAccountsModel
    {
        public string AccountId { get; set; }
        public string Ticker { get; set; }
        public string ShareAmount { get; set; }
        public string AccountType { get; set; }

    }
}
