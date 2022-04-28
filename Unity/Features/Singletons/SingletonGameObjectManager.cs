using System.Collections;

using UnityEngine;

namespace Slaggy.Unity.Singletons
{
    public class SingletonGameObjectManager : MonoBehaviour
    {
        private bool _shuttingDown = false;

        private void Awake() => SingletonMonoBehaviourBase.onDestroyedSingleton += Check;

        // ReSharper disable once DelegateSubtraction
        private void OnDestroy() => SingletonMonoBehaviourBase.onDestroyedSingleton -= Check;

        private void Check()
        {
            if (!_shuttingDown) StartCoroutine(CheckCoroutine());
        }

        private IEnumerator CheckCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            if (_shuttingDown) yield break;

            //Debug.Log("Stuff: " + GetComponentsInChildren<SingletonMonoBehaviourBase>());
            if (GetComponentsInChildren<SingletonMonoBehaviourBase>().Length != 0) yield break;

            Debug.Log($"Destroying Singleton Host GameObject: {this}");
            _shuttingDown = true;

            yield return new WaitForSeconds(0.1f);

            Destroy(gameObject);
        }
    }
}