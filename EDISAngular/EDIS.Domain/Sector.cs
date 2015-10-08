namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sector()
        {
            Industries = new HashSet<Industry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectorID { get; set; }

        [Column("Sector")]
        [Required]
        [StringLength(200)]
        public string Sector1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Industry> Industries { get; set; }
    }
}
