using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public enum RiskLevels {
        [Description("Not Set")]
        NotSet = 0,
        
        [Description("Defensive")]
        Defensive = 1,
        
        [Description("Conservative")]
        Conservative = 2,
        
        [Description("Balanced")]
        Balanced = 3,
        
        [Description("Assertive")]
        Assertive = 4,
        
        [Description("Aggressive")]
        Aggressive = 5


    }
}
