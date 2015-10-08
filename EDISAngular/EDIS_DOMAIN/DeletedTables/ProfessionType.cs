namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfessionType")]
    public partial class ProfessionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfessionTypeId { get; set; }

        [Column("ProfessionType")]
        [StringLength(128)]
        public string ProfessionType1 { get; set; }
    }
}
