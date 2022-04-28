using UnityEngine;

namespace Slaggy.Unity.Singletons
{
    public abstract class SingletonReplaceMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance { get; private set; } = null;

        protected virtual void Awake() => instance = this as T;
        protected virtual void OnApplicationQuit() => instance = null;
        protected virtual void OnDestroy() => instance = null;
    }
}