using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum NoteTypes
    {
        [Description("Message")]
        Message = 1,
        [Description("Email")]
        Email = 2,
        [Description("AccountNote")]
        AccountNote = 3,
        [Description("Voice")]
        Voice = 4
    }
}
