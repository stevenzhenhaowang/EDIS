using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public enum UploadDataTypes {
        [Description("Equity Info")]
        EquityInfo = 1,
        [Description("Bond Info")]
        BondInfo = 2,
        [Description("Asset Price")]
        AssetPrice = 3,
        [Description("Research Value")]
        ResearchValue = 4
    }
}
