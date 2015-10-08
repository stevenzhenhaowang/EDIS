using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class Notes
    {
        [Key]
        public string NoteId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int NoteType { get; set; }
        [Required]
        public int SenderRole { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public string AdviserId { get; set; }
        public string AssetClass { get; set; }
        public string ProductClass { get; set; }
        public string Product { get; set; }
        public string Purpose { get; set; }
        public float TimeSpend { get; set; }
        public string NoteSerial { get; set; }
        public string Body { get; set; }
        public string FollowupActions { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime FollowupDate { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Status { get; set; }
        public bool Reminder { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
        public string AssetTypeId { get; set; }
        public string AccountId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}
