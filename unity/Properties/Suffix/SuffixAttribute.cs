using UnityEngine;

namespace Slaggy.Unity.Properties
{
    public sealed class SuffixAttribute : PropertyAttribute
    {
        public string Suffix { get; } = default;
        public SuffixAttribute(string suffix) => Suffix = suffix;
    }
}
