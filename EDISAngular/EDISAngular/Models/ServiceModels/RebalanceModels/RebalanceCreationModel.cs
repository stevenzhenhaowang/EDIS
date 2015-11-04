using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class RebalanceCreationModel
    {
        public string modelId { get; set; }

        [Required]
        public string name { get; set; }

        #region added property 26/05/2015 
        [Required]
        public string clientGroupId { get; set; }
        #endregion

        //profile must exist validation
        [Required]
        public string profileId { get; set; }
        public List<RebalanceModelParameters> parameters { get; set; }
    }


    public class RebalanceModelParameters
    {
        [Required]
        public string parameterName { get; set; }
        [Required]
        public string parameterId { get; set; }
        [Required]
        public double? weighting { get; set; }
        
        public string identityMetaKey { get; set; }
    }

}