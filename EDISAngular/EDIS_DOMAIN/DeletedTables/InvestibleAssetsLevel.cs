namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestibleAssetsLevel")]
    public partial class InvestibleAssetsLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvestibleAssetsLevelId { get; set; }

        [Column("InvestibleAssetsLevel")]
        [StringLength(256)]
        public string InvestibleAssetsLevel1 { get; set; }
    }
}
