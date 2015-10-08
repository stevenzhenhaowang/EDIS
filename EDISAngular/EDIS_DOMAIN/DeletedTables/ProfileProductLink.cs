namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProfileProductLink
    {
        [Required]
        [StringLength(128)]
        public string RiskProfileID { get; set; }

        public int ProductTypeID { get; set; }

        public int ProfileProductLinkID { get; set; }

        public bool? Selected { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual RiskProfile RiskProfile { get; set; }
    }
}
