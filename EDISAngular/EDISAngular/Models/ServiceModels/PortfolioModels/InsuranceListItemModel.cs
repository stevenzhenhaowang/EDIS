using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class InsuranceListItemModel
    {
        public string insuranceMetaType { get; set; }
        public List<InsuranceListItemDetailModel> data { get; set; }
    }

    public class InsuranceListItemDetailModel
    {
        public string type { get; set; }
        public string insurer { get; set; }
        public string typeOfPolicy { get; set; }
        public string policyNumber { get; set; }
        public string nameOfPolicy { get; set; }
        public string policyAddress { get; set; }
        public string assetInsured { get; set; }
        public double amountInsured { get; set; }
        public double premium { get; set; }
        public DateTime premiumExpiry { get; set; }
        public string days { get; set; }

    }


}