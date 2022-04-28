using Slaggy.Unity.Properties;

using UnityEngine;

namespace Slaggy.Unity.Examples
{
    public class ExampleSuffixScript : MonoBehaviour
    {
        [Suffix("ms")] public float millseconds = 2f;
        [Suffix("s")] public float seconds = 1f;
        [Suffix("m")] public float minutes = 0.5f;
        [Suffix("minutes")] public int minutesLong = 10;
        [Suffix("km")] public float kilometres = 1f;

        // Has to be last apparently
        [Range(0, 10), Suffix("%")] public float percent = 1f;

        [Suffix("General Suffix")] public string text = "Test String.";
        [Suffix("UU")] public int[] numbers = new int[2];
    }
}