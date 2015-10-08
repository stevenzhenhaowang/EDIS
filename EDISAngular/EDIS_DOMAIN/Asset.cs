using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIS_DOMAIN
{
    public partial class Asset
    {
        [Key]
        public string AssetID { get; set; }

        public string Ticker { get; set; }

        public int BalanceType { get; set; }

        public int AssetTypeID { get; set; }

        public string Address { get; set; }

        public int SectorID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }


    }
}
