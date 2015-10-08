namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EducationRecord
    {
        public string EducationRecordID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string EducationLevel { get; set; }

        public int? YearStarted { get; set; }

        public int? YearCompleted { get; set; }

        public int? YearsSinceCompletion { get; set; }

        [StringLength(100)]
        public string EducationInstitution { get; set; }

        [StringLength(100)]
        public string CourseAttended { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

      
    }
}
