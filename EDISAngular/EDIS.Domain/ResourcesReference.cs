using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIS_DOMAIN
{
    public class ResourcesReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string tokenValue { get; set; }
        public string resourceUrl { get; set; }
        public string UserId { get; set; }
        public string fileExtension { get; set; }
    }
}
