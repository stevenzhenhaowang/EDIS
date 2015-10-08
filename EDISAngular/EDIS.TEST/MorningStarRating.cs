namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MorningStarRating
    {
        public string MorningStarRatingID { get; set; }

        [Required]
        [StringLength(128)]
        public string StockID { get; set; }

        public int MorningStarCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string RatingOverall { get; set; }

        [Required]
        [StringLength(50)]
        public string AnalystRating { get; set; }

        [Required]
        [StringLength(50)]
        public string Rating3Yr { get; set; }

        [Required]
        [StringLength(50)]
        public string Rating5Yr { get; set; }

        [Required]
        [StringLength(50)]
        public string Rating10Yr { get; set; }

        [Column(TypeName = "date")]
        public DateTime RatingDate { get; set; }

        [Required]
        [StringLength(50)]
        public string RiskOverall { get; set; }

        [Required]
        [StringLength(50)]
        public string Risk3Yr { get; set; }

        [Required]
        [StringLength(50)]
        public string Risk5Yr { get; set; }

        [Required]
        [StringLength(50)]
        public string Risk10Yr { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual MorningStarCategory MorningStarCategory { get; set; }

    }
}
