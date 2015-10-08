namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientFileInformation
    {
        [Key]
        public string ClientFileID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public int ClientActionID { get; set; }

        [Required]
        [StringLength(100)]
        public string Response { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ResponseDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual ClientFileInformation ClientFileInformations1 { get; set; }

        public virtual ClientFileInformation ClientFileInformation1 { get; set; }
    }
}
