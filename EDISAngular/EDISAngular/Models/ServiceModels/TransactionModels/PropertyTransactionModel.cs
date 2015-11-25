using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.TransactionModels {
    public class PropertyTransactionModel {
        public PropertyAccountView Account { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyType { get; set; }
        public double PropertyPrice { get; set; }
        public double LoanAmount { get; set; }
        public double LoanRate { get; set; }
        public string TypeOfRate { get; set; }
        public double TransactionFee { get; set; }
        public string Institution { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime GrantedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class PropertyAccountView {
        public string id { get; set; }
        public string name { get; set; }
        public string accountCatagory { get; set; }
    }
}