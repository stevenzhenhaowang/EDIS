using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class ReinvestmentActionCreationModel
    {
        [Required]
        public string actionName { get; set; }
        [Required]
        public string Ticker { get; set; }
        [Required]
        public string reinvestmentShareAmount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? reinvestmentDate { get; set; }


        public List<ReinvestmentPlanParticipants> Participants { get; set; }
        


    }



    public class ReinvestmentPlanParticipants {
        public string AccountNumber { get; set; }


    }
}