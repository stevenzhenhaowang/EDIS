namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_AdvisoryNeeds
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(1000)]
        public string ReasonForSeekingAdvice { get; set; }

        [StringLength(1000)]
        public string PersonalFinancialGoals { get; set; }

        [StringLength(1000)]
        public string ExpectationsFromAdviser { get; set; }

        [StringLength(1000)]
        public string AdditionalComments { get; set; }

        public int? CommunicationMethodId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
