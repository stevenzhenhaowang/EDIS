using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EDISAngular.Models.ModelValidators;


namespace EDISAngular.Models.BindingModels
{
    public class CorrespondenceNoteBindingModel
    {
        [Required]
        [AdviserAccountNumberMustExisting(ErrorMessage="adviser does not exist")]
        public string adviserNumber { get; set; }
        [Required]
      //  [ClientMustExisting(ErrorMessage="client does not exist")]
        public string clientId { get; set; }

        [AssetTypeMustExisting(ErrorMessage="asset type does not exist")]
        public string assetTypeId { get; set; }
        [ProductTypeMustExisting(ErrorMessage="Product type id is invalid")]
        public string productTypeId { get; set; }

        public double timespent { get; set; }
        public string noteSerial { get; set; }//self generated
        [Required]
        public string subject { get; set; }
        [Required]
        public string body { get; set; }
        public string followupActions { get; set; }
        public DateTime dateDue { get; set; }
        public string status { get; set; }//not supplied from client
        public DateTime followupDate { get; set; }
        public DateTime dateCompleted { get; set; }//not supplied from client
        public bool reminder { get; set; }
        public DateTime reminderDate { get; set; }//not supplied from client
        [Required]
        [NoteTypeMustExisting(ErrorMessage="Note type supplied does not exist in database")]
        public int noteTypeId { get; set; }
        public bool isAccepted { get; set; }
        public bool isDeclined { get; set; }
        public string accountId { get; set; }

        [Required]
     //   [ResourceTokenMustExisting(ErrorMessage="Resource token is invalid")]
        public string resourceToken { get; set; }




    }
}