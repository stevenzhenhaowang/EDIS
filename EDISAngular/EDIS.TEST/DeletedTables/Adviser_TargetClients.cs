namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adviser_TargetClients
    {
        [StringLength(128)]
        public string UserId { get; set; }

        public int? AnnualIncomeLevelsId { get; set; }

        public int? InvestibleAssetsLevelId { get; set; }

        public int? TotalAssetsLevelId { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int id { get; set; }
    

    }
    
    class Test{
        
    }
    
}
