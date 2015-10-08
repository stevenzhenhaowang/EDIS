namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GlobalCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GlobalCategoryID { get; set; }

        public int GlobalCategoryGroupID { get; set; }

        [Column("GlobalCategory")]
        [Required]
        [StringLength(200)]
        public string GlobalCategory1 { get; set; }
    }
}
