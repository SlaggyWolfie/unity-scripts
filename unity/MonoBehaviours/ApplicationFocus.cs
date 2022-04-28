using UnityEngine;

namespace Slaggy.Unity.Utility
{
    /// <summary>
    /// Class to detect if the application has gained or lost focus
    /// </summary>
    public class ApplicationFocus : MonoBehaviour
    {
        public static bool isFocused = true;
        private void OnApplicationFocus(bool hasFocus) => isFocused = hasFocus;
    }
}
