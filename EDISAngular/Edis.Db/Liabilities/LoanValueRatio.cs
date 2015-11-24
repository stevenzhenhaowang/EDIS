using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db.Liabilities
{
    public class LoanValueRatio
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public double MaxRatio { get; set; }
        [Required]
        public string Ticker { get; set; }
        public AssetTypes AssetTypes { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        public DateTime? ActiveDate { get; set; }
    }
}
