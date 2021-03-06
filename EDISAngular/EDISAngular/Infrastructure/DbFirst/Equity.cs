//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDISAngular.Infrastructure.DbFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equity()
        {
            this.AssetPrices = new HashSet<AssetPrice>();
            this.Dividends = new HashSet<Dividend>();
            this.EquityTransactions = new HashSet<EquityTransaction>();
            this.ResearchValues = new HashSet<ResearchValue>();
        }
    
        public string AssetId { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public int EquityType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetPrice> AssetPrices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dividend> Dividends { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquityTransaction> EquityTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearchValue> ResearchValues { get; set; }
    }
}
