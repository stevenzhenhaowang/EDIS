namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsletterService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewsletterServicesId { get; set; }

        [StringLength(64)]
        public string NewsletterServices { get; set; }
    }
}
