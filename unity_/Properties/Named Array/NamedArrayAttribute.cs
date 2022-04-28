using UnityEngine;

namespace Slaggy.Unity.Properties
{
    /// <summary>
    /// Provides a named array attribute
    /// </summary>
    public class NamedArrayAttribute : PropertyAttribute
    {
        public readonly string[] names;
        public NamedArrayAttribute(string[] names) => this.names = names;
    }
}