// source: https://wiki.unity3d.com/index.php/Singleton

using UnityEngine;

namespace Slaggy.Unity.Singletons
{
    /// <summary>
    ///     Inherit from this base class to create a singleton.
    ///     e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public abstract class SingletonStandalone<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Check to see if we're about to be destroyed.
        private static bool _shuttingDown = false;
        private static object _lock = new object();
        private static T _instance = null;

        /// <summary>
        ///     Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance != null) return _instance;

                    // Search for existing instance.
                    _instance = (T) FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (_instance != null) return _instance;

                    // Need to create a new GameObject to attach the singleton to.
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);

                    return _instance;
                }
            }
        }

        protected virtual void OnApplicationQuit() => _shuttingDown = true;
        protected virtual void OnDestroy() => _shuttingDown = true;
    }
}
