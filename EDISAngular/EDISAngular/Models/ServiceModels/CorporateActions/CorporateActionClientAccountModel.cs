using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class CorporateActionClientAccountModel
    {
        public string edisAccountNumber { get; set; }//account id
        public string accountName { get; set; }
        //public string brokerAccountNumber { get; set; }
        //public string brokerHinSrn { get; set; }
        public string type { get; set; }
        public string shareAmount { get; set; }


    }
}