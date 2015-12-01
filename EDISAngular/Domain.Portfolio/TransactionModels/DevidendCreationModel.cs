using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.TransactionModels
{
    public class DevidendCreationModel
    {
        public string Ticker { get; set; }
        public string Amount { get; set; }
        public DateTime PaymentOn { get; set; }
        public string AddtionalInfo { get; set; }
        public DividendAccountView Account { get; set; }
    }

    public class DividendAccountView {
        public string id { get; set; }
        public string name { get; set; }
        public string accountCatagory { get; set; }
    }
}
