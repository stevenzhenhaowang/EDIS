namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_NewsletterServices
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? NewsletterServicesId { get; set; }

        [StringLength(5)]
        public string IsSubscribed { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
