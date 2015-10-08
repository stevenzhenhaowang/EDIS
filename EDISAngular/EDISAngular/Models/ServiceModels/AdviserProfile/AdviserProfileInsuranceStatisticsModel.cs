using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class ProfileInsuranceStatisticsModel
    {
        public List<ProfileInsuranceStatisticsGroup> group { get; set; }
        public double total { get; set; }
        public List<DataNameAmountPair> data { get; set; }
    }



    public class ProfileInsuranceStatisticsGroup
    {
        public List<DataNameAmountPair> stat { get; set; }
        public string name { get; set; }
    }


}