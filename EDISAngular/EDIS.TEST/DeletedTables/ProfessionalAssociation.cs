namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProfessionalAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfessionalAssociationsId { get; set; }

        [StringLength(128)]
        public string ProfessionalAssociations { get; set; }
    }
}
