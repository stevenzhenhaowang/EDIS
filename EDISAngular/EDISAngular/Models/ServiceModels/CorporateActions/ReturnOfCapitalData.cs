using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class ReturnOfCapitalData
    {       
        public string actionName { get; set; }
        public string accountNumber { get; set; }
        public string returnAmount { get; set; }
        public DateTime returnDate { get; set; }
        public string ticker { get; set; }
       
    }


   

}
