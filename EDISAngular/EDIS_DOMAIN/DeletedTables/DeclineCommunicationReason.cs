namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DeclineCommunicationReason
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeclineCommunicationReasonsId { get; set; }

        [StringLength(512)]
        public string DeclineCommunicationReasons { get; set; }
    }
}
