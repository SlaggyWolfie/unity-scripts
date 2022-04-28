using UnityEngine;

namespace Slaggy.Unity.Properties
{
    public enum DurationUnits
    {
        Seconds,
        Milliseconds,
        Minutes,
        Hours,
        Flexible
    }

    public class DurationAttribute : PropertyAttribute
    {
        private DurationUnits units = default;
        public DurationUnits Units => units;

        public DurationAttribute(DurationUnits units = DurationUnits.Flexible)
            => this.units = units;
    }
}
