namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statement
    {
        public string StatementID { get; set; }

        public int AssetTypeID { get; set; }

        [Required]
        [StringLength(128)]
        public string AccountID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StatementDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public decimal Balance { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }

        public DateTime DateCreated { get; set; }

        public decimal Amount { get; set; }

        public virtual AssetType AssetType { get; set; }
    }
}
