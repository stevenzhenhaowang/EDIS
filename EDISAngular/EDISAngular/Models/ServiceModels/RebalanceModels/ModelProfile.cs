using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class ModelProfile
    {
        public string profileName { get; set; }//aggressive/neutral/defensive
        public string profileId { get; set; }
    }
}