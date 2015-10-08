namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommunicationMethod")]
    public partial class CommunicationMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommunicationMethodId { get; set; }

        [Column("CommunicationMethod")]
        [StringLength(32)]
        public string CommunicationMethod1 { get; set; }
    }
}
