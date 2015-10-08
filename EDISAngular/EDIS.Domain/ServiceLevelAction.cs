namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceLevelAction
    {
        public string ServiceLevelActionID { get; set; }

        [Required]
        [StringLength(128)]
        public string ClientGroupID { get; set; }

        [Required]
        [StringLength(10)]
        public string PersonalProfileUpdated { get; set; }

        [Column(TypeName = "date")]
        public DateTime PersonalProfileUpdateDate { get; set; }

        [Required]
        [StringLength(10)]
        public string FinancialInfoProvided { get; set; }

        [Column(TypeName = "date")]
        public DateTime FinancialInfoProvidedDate { get; set; }

        [Required]
        [StringLength(10)]
        public string LastContactCallMade { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastContactCallMadeDate { get; set; }

        [Required]
        [StringLength(10)]
        public string FeeDisclosureStatement { get; set; }

        [Column(TypeName = "date")]
        public DateTime FeeDisclosureStatementDate { get; set; }

        [Required]
        [StringLength(10)]
        public string OngoingStatementSigned { get; set; }

        [Column(TypeName = "date")]
        public DateTime OngoingStatementSignedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastStatementOfAdviceDate { get; set; }

        public int NumStatementOfAdvice { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastRecordOfAdviceDate { get; set; }

        public int NumRecordOfAdvice { get; set; }

        public int NumAnnualReviewMeeting { get; set; }

        public int NumPhoneContacts { get; set; }

        public int NumInvestmentReports { get; set; }

        public int NumAdviceCalls { get; set; }

        public int NumSeminars { get; set; }

        public int NumMonthlyBulletin { get; set; }

        public int NumWeeklyBulletin { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
