using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db {
    public class CountryCode {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string CountryName{ get; set; }
    }
}
