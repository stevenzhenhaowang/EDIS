namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dependant
    {
        public string DependantID { get; set; }

        [Required]
        [StringLength(128)]
        public string DependantClientID { get; set; }

        [Required]
        [StringLength(128)]
        public string ParentClientID { get; set; }  

        [StringLength(100)]
        public string Relationship { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        
    }
}
