namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        public string AppointmentID { get; set; }

        [StringLength(128)]
        public string ClientID { get; set; }

        [Required]
        [StringLength(128)]
        public string AdviserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime AppointmentTime { get; set; }

        [StringLength(50)]
        public string AppointmentType { get; set; }

        public decimal DurationHours { get; set; }

        [Required]
        [StringLength(100)]
        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Client Client { get; set; }
    }
}
