using System.ComponentModel;

namespace Shared
{
    public enum RebalanceModelProfile
    {
        [Description("Aggressive")]
        Aggressive = 1,
        [Description("Neutral")]
        Neutral = 2,
        [Description("Defensive")]
        Defensive = 3
    }
}
