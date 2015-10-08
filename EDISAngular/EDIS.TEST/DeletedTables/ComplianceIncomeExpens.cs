namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ComplianceIncomeExpenses")]
    public partial class ComplianceIncomeExpens
    {
        [Key]
        public string ComplianceIncomeExpensesID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientID { get; set; }

        public decimal? Centrelink { get; set; }

        public decimal? ReceivedMaintenance { get; set; }

        public decimal? SuperannunationPension { get; set; }

        public decimal? OtherTaxableIncome { get; set; }

        public decimal? OtherForeignIncome { get; set; }

        public decimal? NonTaxableIncome { get; set; }

        public decimal? FoodExpenses { get; set; }

        public decimal? ClothingExpenses { get; set; }

        public decimal? MedicalExpenses { get; set; }

        public decimal? UtilitiesBills { get; set; }

        public decimal? CommunicationsBills { get; set; }

        public decimal? Furniture { get; set; }

        public decimal? MortgageRental { get; set; }

        public decimal? HomeInsurance { get; set; }

        public decimal? VehicleInsurance { get; set; }

        public decimal? Repairs { get; set; }

        public decimal? CouncilRates { get; set; }

        public decimal? VehicleRegistration { get; set; }

        public decimal? Petrol { get; set; }

        public decimal? VehicleLoans { get; set; }

        public decimal? Entertainment { get; set; }

        public decimal? HolidayTravel { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Client Client { get; set; }
    }
}
