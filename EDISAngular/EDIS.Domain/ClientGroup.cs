namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientGroup
    {
        public string ClientGroupID { get; set; }

        public string AccountName { get; set; }

        public int AdviserNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Alias { get; set; }       

        public int? RiskProfileOutcome { get; set; }

        public int? ServiceLevelID { get; set; }

        [StringLength(128)]
        public string MainClientID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual ServiceLevel ServiceLevel { get; set; }
    }
}
