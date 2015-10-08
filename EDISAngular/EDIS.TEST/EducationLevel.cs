namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EducationLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EducationLevelsId { get; set; }

        [StringLength(64)]
        public string EducationLevels { get; set; }
    }
}
