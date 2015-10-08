
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{

    public class InsuranceStatModel
    {
        public List<InsuranceStateItem> data { get; set; }
        public double total { get; set; }
    }

    public class InsuranceStateItem
    {
        public string Type { get; set; }
        public List<BusinessStatDataItem> Content { get; set; }
    }


    public class BusinessStatDetailModel
    {
        public List<BusinessStatDataItem> data { get; set; }
        public double total { get; set; }
    }


    public class BusinessStatDataItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double PL { get; set; }
        public List<BusinessStatStock> Stock { get; set; }
    }


    public class BusinessStatStock
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public int Units { get; set; }
        public double Value { get; set; }
        public double PL { get; set; }
        public List<BusinessStatStockAccount> Accounts { get; set; }
    }

    public class BusinessStatStockAccount
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int Units { get; set; }
        public double Value { get; set; }
        public double PL { get; set; }

    }


}