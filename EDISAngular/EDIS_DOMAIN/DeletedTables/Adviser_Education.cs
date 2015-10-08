namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_Education
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(64)]
        public string EducationLevels { get; set; }

        [StringLength(128)]
        public string Institution { get; set; }

        [StringLength(128)]
        public string CourseName { get; set; }

        public int? CurrentlyStudying { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
