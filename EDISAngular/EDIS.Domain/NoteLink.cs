using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EDIS_DOMAIN
{
    public class NoteLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime DateCreated { get; set; }
        public string NoteID1 { get; set; }
        public string NoteID2 { get; set; }
    }
}
