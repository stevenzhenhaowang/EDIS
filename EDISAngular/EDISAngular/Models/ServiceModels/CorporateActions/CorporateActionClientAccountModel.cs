using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class CorporateActionClientAccountModel
    {
        public string edisAccountNumber { get; set; }//client account id
        //public string brokerAccountNumber { get; set; }
        //public string brokerHinSrn { get; set; }
        public string type { get; set; }
        public string name { get; set; }


    }
}