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
    
    public partial class BondTransaction
    {
        public string Id { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int NumberOfUnits { get; set; }
        public double UnitPriceAtPurchase { get; set; }
        public string BondId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Account_AccountId { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Bond Bond { get; set; }
    }
}
