namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {            
           
            Notes = new HashSet<Note>();
            Policies = new HashSet<Policy>();
            RiskProfiles = new HashSet<RiskProfile>();
        }

       

        [Required]
        [StringLength(128)]
        [Key]
        public string ClientUserID { get; set; }

        [StringLength(128)]
        public string ClientGroupID { get; set; }
      
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastContact { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastReview { get; set; }

        public int? RiskProfileOutcome { get; set; }

        public byte[] Image { get; set; }

        [StringLength(256)]
        public string ImageMimeType { get; set; }       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Dependant> Dependants { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policy> Policies { get; set; }      

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RiskProfile> RiskProfiles { get; set; }
     
    }
}
