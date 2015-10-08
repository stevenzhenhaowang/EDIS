using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace EDIS_DOMAIN.Enum.Enums
{
    public enum EducationLevels
    {
        [Description("Diploma")]
        Diploma=1,
        [Description("Bachelor")]
        Bachelor=2,
        [Description("Masters")]
        Masters=3,
        [Description("Doctorate")]
        Doctorate=4,
        [Description("Associate Professor")]
        AssociateProfessor=5,
        [Description("Professor")]
        Professor=6,
        [Description("Other")]
        Other=7
    }
}