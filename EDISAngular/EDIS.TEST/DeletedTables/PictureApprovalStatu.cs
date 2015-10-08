namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PictureApprovalStatu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PictureApprovalStatusId { get; set; }

        [StringLength(32)]
        public string PictureApprovalStatus { get; set; }
    }
}
