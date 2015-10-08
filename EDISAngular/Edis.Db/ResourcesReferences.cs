using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class ResourcesReferences
    {
        [Key]
        public string Id { get; set; }
        public string TokenValue { get; set; }
        public string FileExtension { get; set; }
        public string ResourceUrl { get; set; }
        public string UserId { get; set; }
    }
}
