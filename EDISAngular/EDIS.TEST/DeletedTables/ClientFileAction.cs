namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientFileAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientFileActionID { get; set; }

        [Column("ClientFileAction")]
        [Required]
        [StringLength(500)]
        public string ClientFileAction1 { get; set; }
    }
}
