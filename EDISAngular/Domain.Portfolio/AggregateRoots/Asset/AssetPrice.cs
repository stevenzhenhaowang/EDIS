using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.AggregateRoots.Asset
{

    public class AssetPrice
    {
        public string Id { get; set; }
        public double? Price { get; set; }
        public DateTime? CreatedOn { get; set; }
        public AssetTypes AssetType { get; set; }
        public string CorrespondingAssetKey { get; set; }


    }
}
