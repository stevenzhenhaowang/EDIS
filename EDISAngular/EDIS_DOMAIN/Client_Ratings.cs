namespace EDIS_DOMAIN
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_Ratings
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string UserId_Adviser { get; set; }

        public double? Feedback_Q1 { get; set; }

        [StringLength(500)]
        public string Feedback_Q1_Comments { get; set; }

        public double? Feedback_Q2 { get; set; }

        [StringLength(500)]
        public string Feedback_Q2_Comments { get; set; }

        public double? Feedback_Q3 { get; set; }

        [StringLength(500)]
        public string Feedback_Q3_Comments { get; set; }

        public double? Feedback_Q4 { get; set; }

        [StringLength(500)]
        public string Feedback_Q4_Comments { get; set; }

        public double? Feedback_Q5 { get; set; }

        [StringLength(500)]
        public string Feedback_Q5_Comments { get; set; }

        [StringLength(500)]
        public string AdditionalComments { get; set; }

        [StringLength(5)]
        public string IsSuccessReferral { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
