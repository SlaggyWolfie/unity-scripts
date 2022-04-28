using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slaggy.Unity
{
    /// <summary>
    /// Utility class that provides helper functions
    /// </summary>
    public partial class Utilities
    {
        /// <summary>
        /// Check if cursor is over UI. Only works if the Canvasgroup blocks Raycasts.
        /// </summary>
        /// <returns></returns>
        public static bool IsCursorOverUI() => EventSystem.current.IsPointerOverGameObject() || GUIUtility.hotControl != 0;

        public static bool AnyKeyUpAmong(KeyCode[] keys) => keys.Any(Input.GetKeyUp);
        public static bool AnyKeyDownAmong(KeyCode[] keys) => keys.Any(Input.GetKeyDown);
    }
}
