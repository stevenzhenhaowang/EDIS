namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class aspnet_Applications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aspnet_Applications()
        {
            aspnet_Users = new HashSet<aspnet_Users>();
        }

        [Required]
        [StringLength(256)]
        public string ApplicationName { get; set; }

        [Required]
        [StringLength(256)]
        public string LoweredApplicationName { get; set; }

        [Key]
        public string ApplicationId { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aspnet_Users> aspnet_Users { get; set; }
    }
}
