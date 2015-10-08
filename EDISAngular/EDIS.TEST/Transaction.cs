namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public string TransactionID { get; set; }
        public int TransactionTypeID { get; set; }
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }
        [Required]
        [StringLength(100)]
        public string ContractNo { get; set; }
        public decimal TransactionFee { get; set; }        
        [Required]
        [StringLength(128)]
        public string AccountID { get; set; }
        public int NoOfUnits { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual AssetType AssetType { get; set; }
    }
}
