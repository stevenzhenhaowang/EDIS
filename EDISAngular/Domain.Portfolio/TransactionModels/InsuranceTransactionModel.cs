using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.TransactionModels
{
    public class InsuranceTransactionModel
    {
        public InsuranceType insuranceType { get; set; }
        public string insuranceAmount { get; set; }
        public bool isAquired { get; set; }
        public PolicyType policyType { get; set; }
        public string policyNumber { get; set; }
        public string policyAddress { get; set; }
        public string premium { get; set; }
        public string issuer { get; set; }
        public string insuredEntity { get; set; }
        public DateTime grantedDate { get; set; }
        public DateTime expiryDate { get; set; }
        public InsuranceAccountView account { get; set; }

    }

    public class InsuranceAccountView
    {
        public string id { get; set; }
        public string name { get; set; }
        public string accountCatagory { get; set; }
    }
}
