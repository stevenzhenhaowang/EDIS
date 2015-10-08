namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FinancialHealthGrade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FinancialHealthGrade()
        {
            FundStocks = new HashSet<FundStock>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinancialHealthGradeID { get; set; }

        [Column("FinancialHealthGrade")]
        [Required]
        [StringLength(200)]
        public string FinancialHealthGrade1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FundStock> FundStocks { get; set; }
    }
}
