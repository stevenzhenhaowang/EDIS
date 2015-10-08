namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_ServicesProviding
    {
        [StringLength(128)]
        public string UserId { get; set; }

        public int? SubServiceId { get; set; }

        [StringLength(5)]
        public string SubServiceProvideYesNo { get; set; }

        public DateTime? DateAdded { get; set; }

        public int id { get; set; }
    }
}
