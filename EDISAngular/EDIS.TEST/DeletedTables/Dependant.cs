namespace EDIS.Test
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
        public string ClientID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Relationship { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public bool FinancialDependant { get; set; }

        public double? DependantIncome { get; set; }

        public double? DependantAsset { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public virtual Client Client { get; set; }
    }
}
