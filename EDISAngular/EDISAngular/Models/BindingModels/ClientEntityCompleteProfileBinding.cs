using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace EDISAngular.Models.BindingModels
{
    public class ClientEntityCompleteProfileBinding
    {
        [Required]
        [HiddenInput(DisplayValue=false)]
        public string UserID { get; set; }

        [Required]
        [Display(Name="Entity Name")]
        public string EntityName { get; set; }
        //[Required]
        ////[DataType(DataType.PhoneNumber)]
        //[Display(Name="Mobile")]
        //public string Mobile { get; set; }
        [Required]
        [Display(Name="Phone")]
        //[DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        //[DataType(DataType.PhoneNumber)]
        [Display(Name="Fax")]
        public string Fax { get; set; }
        [Required]
        [Display(Name="Address Line 1")]
        public string line1 { get; set; }
        [Display(Name="Address Line 2")]
        public string line2 { get; set; }
        [Display(Name="Address Line 3")]
        public string line3 { get; set; }

        [Required]
        [Display(Name="Suburb")]
        public string Suburb { get; set; }
        [Required]
        [Display(Name="State")]
        public string State { get; set; }
        [Required]
        [Display(Name="Post Code")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        [Required]
        [Display(Name="Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name="Type of Entity")]
        public string EntityType { get; set; }
        [Required]
        [Display(Name="ABN")]
        public string ABN { get; set; }
        [Required]
        [Display(Name="ACN")]
        public string ACN { get; set; }



    }
}