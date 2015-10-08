namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PolicyDetail
    {
        public string PolicyDetailID { get; set; }

        [Required]
        [StringLength(128)]
        public string PolicyID { get; set; }

        [Required]
        [StringLength(100)]
        public string DetailType { get; set; }

        [StringLength(100)]
        public string DetailName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateModified { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
