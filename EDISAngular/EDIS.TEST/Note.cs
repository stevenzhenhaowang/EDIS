namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Note
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Note()
        {
            Attachments = new HashSet<Attachment>();
        }

        public string NoteID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Required]
        [StringLength(128)]
        public string AdviserID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        [StringLength(100)]
        public string AssetClass { get; set; }

        [StringLength(100)]
        public string ProductClass { get; set; }

        [StringLength(100)]
        public string Product { get; set; }

        [StringLength(100)]
        public string Purpose { get; set; }

        public double? TimeSpent { get; set; }

        [StringLength(100)]
        public string NoteSerial { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        public string Body { get; set; }

        [StringLength(100)]
        public string FollowupActions { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateDue { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FollowupDate { get; set; }

        public DateTime? DateCompleted { get; set; }

        public bool? Reminder { get; set; }

        public DateTime? ReminderDate { get; set; }

        public int NoteTypesID { get; set; }

        public bool? IsAccepted { get; set; }

        public bool? IsDeclined { get; set; }

        public int? AssetTypeID { get; set; }

        [StringLength(128)]
        public string AccountID { get; set; }

        public int SenderRole { get; set; }

        public virtual AssetType AssetType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }

        public virtual Client Client { get; set; }


    }
}
