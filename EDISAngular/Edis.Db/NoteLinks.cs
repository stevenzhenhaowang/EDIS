using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class NoteLinks
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public string NoteId1 { get; set; }
        public string NoteId2 { get; set; }
    }
}
