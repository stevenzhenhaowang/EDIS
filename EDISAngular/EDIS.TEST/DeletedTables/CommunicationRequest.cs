namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommunicationRequest")]
    public partial class CommunicationRequest
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Adviser_UserId { get; set; }

        [StringLength(128)]
        public string Client_UserId { get; set; }

        public bool? AcceptOrDecline { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? DeclineCommunicationReasonsId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ProcessDate { get; set; }
    }
}
