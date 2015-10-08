namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmploymentRecord
    {
        public string EmploymentRecordID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string EmploymentType { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public decimal? HoursPerWeek { get; set; }

        public double? GrossSalary { get; set; }

        public double? Commissions { get; set; }

        public double? AfterTaxSalary { get; set; }

        public double? SuperContribution { get; set; }

        public double? AdditionalSuperContribution { get; set; }

        public double? MiscContribution { get; set; }

        public double? FBT { get; set; }

        public double? EmployeeSharePlan { get; set; }

        public decimal? SickLeave { get; set; }

        public decimal? AnnualLeave { get; set; }

        public decimal? LongServiceLeave { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
