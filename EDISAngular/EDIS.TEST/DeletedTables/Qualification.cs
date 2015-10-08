namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Qualification
    {
        public string QualificationID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Column("Qualification")]
        [Required]
        [StringLength(200)]
        public string Qualification1 { get; set; }

        public int QualificationIndex { get; set; }

        public int QualificationTypeID { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        public virtual QualificationType QualificationType { get; set; }
    }
}
