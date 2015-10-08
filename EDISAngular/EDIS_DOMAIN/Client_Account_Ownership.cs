using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIS_DOMAIN
{
    public partial class Client_Account_Ownership
    {
        public string ClientUserID { get; set; }
        public string AccountID { get; set; }
        public double Percentage { get; set; }
    }
}
