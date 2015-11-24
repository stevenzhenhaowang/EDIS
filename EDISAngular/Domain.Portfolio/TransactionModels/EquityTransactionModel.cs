using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.TransactionModels
{
    public class EquityTransactionModel
    {
        public string Name { get; set; }
        public int NumberOfUnits { get; set; }
        public double Price { get; set; }
        public string Sector { get; set; }
        public string Ticker { get; set; }
        public double LoanAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public EquityAccountView Account { get; set; }
    }

    public class EquityAccountView {
        public string id { get; set; }
        public string name { get; set; }
        public string accountCatagory { get; set; }
    }
}
