using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EDISAngular.Models.ModelValidators;


namespace EDISAngular.Models.BindingModels
{
    public class EntityClientCreationBindingModel
    {
        [Required]
        [DisplayName("Name of Entity")]
        public string nameOfEntity { get; set; }
        [Required]
        [DisplayName("Type of Entity")]
        public string typeOfEntity { get; set; }
        [Required]
        [DisplayName("ABN")]
        public string abn { get; set; }
        [Required]
        [DisplayName("ACN")]
        public string acn { get; set; }
        [Required]
        [DisplayName("Contact Phone")]
        public string contactPhone { get; set; }
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddressMustBeUnique(ErrorMessage = "Email address has already been taken")]
        public string email { get; set; }
        [Required]
        [DisplayName("Group")]
        [ClientGroupMustExisting(ErrorMessage="Cannot find corresponding client group for this entity client")]
        public string existingGroupId { get; set; }
        public RiskProfileBindingModel riskProfile { get; set; }


    }
}