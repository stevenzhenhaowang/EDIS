using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EDISAngular.Models.ModelValidators;


namespace EDISAngular.Models.BindingModels
{
    public class CorrespondenceFollowupBindingModel
    {
        [Required]
        //[NoteMustExisting(ErrorMessage="Follow-up note is not valid")]
        public string existingNoteId { get; set; }
        [Required]
        public string body { get; set; }
    }
}