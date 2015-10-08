namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QualificationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QualificationType()
        {
            Qualifications = new HashSet<Qualification>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QualificationTypeID { get; set; }

        [Column("QualificationType")]
        [Required]
        [StringLength(100)]
        public string QualificationType1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Qualification> Qualifications { get; set; }
    }
}
