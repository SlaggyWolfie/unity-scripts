using System;
using System.Collections;

using UnityEngine;

namespace Slaggy.Unity.Singletons
{
    public abstract class SingletonMonoBehaviourBase : MonoBehaviour
    {
        public static Action onDestroyedSingleton = default;

        protected void DestroySelf()
        {
            Debug.Log($"Destroyed self: {this}");
            Destroy(this);
        }
    }

    public abstract class SingletonMonoBehaviour<T> : SingletonMonoBehaviourBase where T : MonoBehaviour
    {
        public static T instance { get; private set; } = null;

        protected bool ShuttingDown { get; private set; } = false;

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this as T;
            else
                StartCoroutine(DelayedDestroy());
        }

        private IEnumerator DelayedDestroy()
        {
            yield return null;
            DestroySelf();
        }

        protected virtual void OnApplicationQuit()
        {
            ShuttingDown = true;
            Cleanup();
        }

        protected virtual void OnDestroy()
        {
            if (ShuttingDown) return;

            onDestroyedSingleton?.Invoke();
            Cleanup();
        }

        private void Cleanup()
        {
            if (this == instance) instance = null;
            StopAllCoroutines();
        }
    }
}