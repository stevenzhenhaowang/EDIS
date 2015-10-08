namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expenses")]
    public partial class Expens
    {
        [Key]
        public string ExpenseID { get; set; }

        public int ExpenseTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int Frequency { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [Required]
        [StringLength(128)]
        public string AccountID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int AssetTypeID { get; set; }

        public virtual AssetType AssetType { get; set; }

        public virtual ExpenseType ExpenseType { get; set; }
    }
}
