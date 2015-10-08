using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class Attachments
    {
        [Key]
        public string AttachmentId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public string Path { get; set; }
        public DateTime DateModified { get; set; }
        public string Comments { get; set; }
        public byte Data { get; set; }
        public string AttachmentType { get; set; }

        [ForeignKey("NoteId")]
        public virtual Notes Note { get; set; }
        [Required]
        public string NoteId { get; set; }
    }
}
