using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIS.Test
{
    public partial class Research
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public int Key { get; set; }
        public double Value { get; set; }
    }
}
