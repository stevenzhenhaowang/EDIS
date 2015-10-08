using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class InvestmentPortfolioModel
    {
        public List<DataNameAmountPair> data { get; set; }
        public double total { get; set; }
    }
}