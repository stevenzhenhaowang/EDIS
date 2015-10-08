using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum NoteTypes
    {
        [Description("Message")]
        Message=1,
        [Description("Email")]
        Email=2,
        [Description("AccountNote")]
        AccountNote=3,
        [Description("Voice")]
        Voice=4
    }
}