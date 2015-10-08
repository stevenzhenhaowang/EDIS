namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AnnualIncomeLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnnualIncomeLevelsId { get; set; }

        [StringLength(256)]
        public string AnnualIncomeLevels { get; set; }
    }
}
