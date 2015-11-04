using System.ComponentModel;

namespace Shared
{
    public enum RebalanceClassificationType
    {
        [Description("Sectors")]
        Sectors = 1,
        [Description("Countries")]
        Countries = 2
    }
}