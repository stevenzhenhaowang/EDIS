using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.CorperateActions
{
    public class ReinvestmentPlanAction
    {
        [Key]
        public int Id { get; set; }
        public string AdviserId { get; set; }
        public string Ticker { get; set; }
        public double ShareAmount { get; set; }
        public DateTime ReinvestmentDate { get; set; }
        public string ParticipantsAccount { get; set; }
        public CorporateActionStatus Status { get; set; }
    }
}
