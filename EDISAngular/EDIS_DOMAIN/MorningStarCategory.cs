namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MorningStarCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MorningStarCategory()
        {
            MorningStarRatings = new HashSet<MorningStarRating>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MorningStarCategoryID { get; set; }

        [Column("MorningStarCategory")]
        [Required]
        [StringLength(200)]
        public string MorningStarCategory1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MorningStarRating> MorningStarRatings { get; set; }
    }
}
