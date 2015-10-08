using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class BuisnessRevenueDetailsDataModel
    {
        public List<BusinessRevenueItem> data { get; set; }
        public double total { get; set; }
    }

    public class BusinessRevenueItem
    {
        public string name { get; set; }
        public double amount { get; set; }
        public List<BusinessRevenueAccount> Accounts { get; set; }
    }


    public class BusinessRevenueAccount
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AUM { get; set; }
        public double Revenue { get; set; }
    }


}