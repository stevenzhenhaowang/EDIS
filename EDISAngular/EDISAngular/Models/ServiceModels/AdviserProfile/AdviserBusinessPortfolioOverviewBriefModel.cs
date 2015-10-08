using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Models.ServiceModels;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class BusinessPortfolioOverviewBriefModel
    {
        public List<DataNameAmountPair> data { get; set; }
        public double total { get; set; }
    }



}