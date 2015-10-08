namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvestedProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvestedProductsId { get; set; }

        [StringLength(64)]
        public string InvestedProductsName { get; set; }
    }
}
