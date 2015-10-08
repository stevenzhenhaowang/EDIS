namespace EDIS.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WebStats_NumberOfLogins
    {
        [StringLength(256)]
        public string Email { get; set; }

        public DateTime? LoginDate { get; set; }

        public int id { get; set; }
    }
}
