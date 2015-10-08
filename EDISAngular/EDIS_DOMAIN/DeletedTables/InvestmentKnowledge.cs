namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestmentKnowledge")]
    public partial class InvestmentKnowledge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvestmentKnowledgeId { get; set; }

        [Column("InvestmentKnowledge")]
        [StringLength(64)]
        public string InvestmentKnowledge1 { get; set; }
    }
}
