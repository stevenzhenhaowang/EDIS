namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceLevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceLevel()
        {
            ClientGroups = new HashSet<ClientGroup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceLevelID { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceLevelName { get; set; }

        public int ReviewsPerAnnum { get; set; }

        public int ReportsPerAnnum { get; set; }

        public int PhoneContactsPerAnnum { get; set; }

        public int StatementsPerAnnum { get; set; }

        public int RecordOfAdvicePerAnnum { get; set; }

        public int AdviceCallPerAnnum { get; set; }

        public int BulletinsPerAnnum { get; set; }

        public bool AustralianEquityResearch { get; set; }

        public bool InternationalEquityResearch { get; set; }

        public bool ManagedInvestmentResearch { get; set; }

        public bool PropertyResearch { get; set; }

        public bool IPOs { get; set; }

        public bool OnlineAccess { get; set; }

        public int SeminarsPerAnnum { get; set; }

        public decimal InternalAdminCostMin { get; set; }

        public decimal InternalAdminCostMax { get; set; }

        public decimal ExternalAdminCostMin { get; set; }

        public decimal ExternalAdminCostMax { get; set; }

        public decimal OngoingCostMin { get; set; }

        public decimal OngoingCostMax { get; set; }

        public decimal InitialAdviceCost { get; set; }

        public decimal MinBrokerageFee { get; set; }

        public decimal MaxBrokerageFee { get; set; }

        public decimal RecordOfAdviceFee { get; set; }

        public decimal ConsultingFeePerHour { get; set; }

        public decimal AccountingFeePerHour { get; set; }

        public decimal LegalFeesPerHour { get; set; }

        public decimal AuditingFees { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientGroup> ClientGroups { get; set; }
    }
}
