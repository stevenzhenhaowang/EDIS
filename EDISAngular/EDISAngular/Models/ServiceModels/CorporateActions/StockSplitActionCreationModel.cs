using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.CorporateActions
{
    public class StockSplitActionCreationModel
    {
        [Required]
        public string actionName { get; set; }
        [Required]
        public string actionCode { get; set; }
        [Required]
        public string stockSplitShares { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime splitDate { get; set; }
    }
}