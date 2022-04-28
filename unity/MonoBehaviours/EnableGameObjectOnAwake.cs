using UnityEngine;

namespace Slaggy.Utility
{
    public class EnableGameObjectOnAwake : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject = null;

        private void Awake()
        {
            if (_gameObject != null) _gameObject.SetActive(true);
        }
    }
}