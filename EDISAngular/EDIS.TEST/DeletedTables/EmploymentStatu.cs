namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmploymentStatu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmploymentStatusId { get; set; }

        [StringLength(32)]
        public string EmploymentStatus { get; set; }
    }
}
