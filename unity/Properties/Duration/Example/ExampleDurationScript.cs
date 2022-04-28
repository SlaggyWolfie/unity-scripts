using Slaggy.Unity.Properties;

using UnityEngine;

namespace Slaggy.Unity.Examples
{
    public class ExampleDurationScript : MonoBehaviour
    {
        [Duration(DurationUnits.Milliseconds)] public float millseconds = 2f;
        [Duration(DurationUnits.Seconds)] public float seconds = 1f;
        [Duration(DurationUnits.Minutes)] public float minutes = 0.5f;
        [Duration(DurationUnits.Hours)] public float hours = 1f;
        [Duration(DurationUnits.Flexible)] public float flexible = 1f;
        [Duration] public float defaultFlexible = 1f;
    }
}