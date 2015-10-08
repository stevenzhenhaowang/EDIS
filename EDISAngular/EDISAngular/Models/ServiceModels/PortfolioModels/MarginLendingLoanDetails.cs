using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class MarginLendingLoanDetails
    {
        #region modified properties 14/05/2015
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        #endregion
        public List<MarginLendingLoanDetailsItem> data { get; set; }
    }


    public class MarginLendingLoanDetailsItem
    {
        public string ticker { get; set; }
        public string company { get; set; }
        public double netCostValue { get; set; }
        public double marketValue { get; set; }
        public double loanValueRatio { get; set; }
        public double loanAmount { get; set; }
        public string equityInAsset { get; set; }
    }





}