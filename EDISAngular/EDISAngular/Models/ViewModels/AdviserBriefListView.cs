using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace EDISAngular.Models.ViewModels
{
    public class AdviserBriefListView
    {
        
        public string AdviserId { get; set; }
        [Display(Name="Adviser Name")]
        public string Name { get; set; }
        [Display(Name="ACN/ABN")]
        public string AcnAbn { get; set; }
        [Display(Name="Suburb")]
        public string Suburb { get; set; }
        [Display(Name="State")]
        public string State { get; set; }
        [Display(Name="PostCode")]
        public string PostCode { get; set; }

    }
}