using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class BonusIsuueActionCreationModel
    {

        [Required]
        public string actionName { get; set; }
        [Required]
        public string actionCode { get; set; }
        [Required]
        public string bonusIssue { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime bonusDate { get; set; }
    }
}