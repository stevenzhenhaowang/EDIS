using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.CorporateActions
{
    public class ReturnOfCapitalCreationModel
    {
        public string ActionName { get; set; }
        public string EquityId { get; set; }
        public string AdviserId { get; set; }
        public string ShareMount { get; set; }
        public string ReturnOfCapitalAmount { get; set; }
        public DateTime? AdjustmentDate { get; set; }

    }
}
