namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_ProfessionalAssociations
    {
        [StringLength(128)]
        public string UserId { get; set; }

        public int? ProfessionalAssociationsId { get; set; }

        public int id { get; set; }
    }
}
