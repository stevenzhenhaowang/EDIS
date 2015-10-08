using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EDISAngular.Models.ModelValidators;

namespace EDISAngular.Models.BindingModels
{
    public class PersonClientCreationBindingModel
    {
        [Required]
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [DisplayName("Middle Name")]
        public string middleName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Client Email")]
        [EmailAddressMustBeUnique(ErrorMessage="Email address has already been taken")]
        public string email { get; set; }
        [Required]
        [DisplayName("Client Contact Phone")]
        public string contactPhone { get; set; }
        [Required]
        [DisplayName("Group Main Setting")]
        public bool? isGroupLeader { get; set; }
        [DisplayName("New Group Account Name")]
        [RequiredIfBoolTrue("isGroupLeader",ErrorMessage="{0} is required if this client is a group leader")]
        public string newGroupAccountName { get; set; }
        [DisplayName("New Group Alias")]
        [RequiredIfBoolTrue("isGroupLeader", ErrorMessage = "{0} is required if this client is a group leader")]
        public string newGroupAlias { get; set; }
        [DisplayName("New Group Adviser")]
        [RequiredIfBoolTrue("isGroupLeader", ErrorMessage = "{0} is required if this client is a group leader")]
        [AdviserAccountNumberMustExisting(ErrorMessage="Cannot locate corresponding adviser for this person client item")]
        public int newGroupAdviserNumber { get; set; }
        [DisplayName("Add to Group")]
        [RequiredIfBoolFalse("isGroupLeader", ErrorMessage = "{0} is required if this client is a group leader")]
        [ClientGroupMustExisting(ErrorMessage = "Cannot find corresponding client group for this person client")]
        public string existingGroupId { get; set; }
        public RiskProfileBindingModel riskProfile { get; set; }
    }
}