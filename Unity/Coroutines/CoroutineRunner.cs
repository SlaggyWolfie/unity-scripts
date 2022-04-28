using System.Collections;
using UnityEngine;

namespace Slaggy.Unity.CoroutineUtility
{
    public static partial class Coroutines
    {
        public class Runner : MonoBehaviour { }
        private static Runner _runner;

        public static Coroutine RunCoroutine(this IEnumerator enumerator)
        {
            if (_runner != null) return _runner.StartCoroutine(enumerator);

            _runner = new GameObject("[CoroutineHandler]").AddComponent<Runner>();

            _runner.hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy |
                                        HideFlags.HideInInspector | HideFlags.NotEditable | HideFlags.DontSaveInBuild;

            _runner.gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy |
                                                   HideFlags.HideInInspector | HideFlags.NotEditable |
                                                   HideFlags.DontSaveInBuild;

            return _runner.StartCoroutine(enumerator);
        }

        public static Coroutine StartCoroutine(this IEnumerator enumerator, MonoBehaviour monoBehaviour) => monoBehaviour.StartCoroutine(enumerator);
    }
}