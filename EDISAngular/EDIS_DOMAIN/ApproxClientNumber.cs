namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ApproxClientNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApproxClientNumbersId { get; set; }

        [StringLength(32)]
        public string ApproxClientNumbers { get; set; }
    }
}
