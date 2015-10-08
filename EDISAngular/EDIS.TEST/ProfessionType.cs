using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIS.Test
{
    public partial class ProfessionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfessionTypeId { get; set; }

        [Column("ProfessionType")]
        [StringLength(128)]
        public string ProfessionType1 { get; set; }
    }
}
