using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EDISAngular.Models.ModelValidators;
using Shared;

namespace EDISAngular.Models.BindingModels
{
    public class ClientAccountCreationBindingModel
    {
        [Required]
        [DisplayName("Client Group")]
        public string clientGroup { get; set; }
        [Required]
        [DisplayName("Client")]
        public string client { get; set; }
        [Required]
        [DisplayName("Account Type")]
        public AccountType accountType { get; set; }
        [Required]
        [DisplayName("Account Name")]
        public string accountName { get; set; }
    }
}