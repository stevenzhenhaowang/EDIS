using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EDISAngular.Models.ModelValidators;
using System.Web.Mvc;



namespace EDISAngular.Models.BindingModels
{
    public class ClientPersonCompleteProfileBinding
    {
        [Required]
        [HiddenInput(DisplayValue=false)]
        public string UserId { get; set; }

        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        
        [Display(Name="Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Gender")]
        [StringRangeValidator("male,female",ErrorMessage="Gender may only be male or female")]
        public string Gender { get; set; }

        [Required]
        [Display(Name="Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime? DOB { get; set; }

        [Required]
        //[DataType(DataType.PhoneNumber)]                          //CSS
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Phone")]
        //[DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        //[DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string line1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string line2 { get; set; }
        [Display(Name = "Address Line 3")]
        public string line3 { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }





    }
}