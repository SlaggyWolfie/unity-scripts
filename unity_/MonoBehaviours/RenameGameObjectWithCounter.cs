using UnityEngine;

namespace Slaggy.Unity.Utility
{
    public class RenameGameObjectWithCounter : MonoBehaviour
    {
        private static int _counter = 0;

        [SerializeField] private string _newName = string.Empty;

        private void Start()
        {
            if (!string.IsNullOrEmpty(_newName))
                gameObject.name = $"{_newName} {_counter++}";
            else gameObject.name += $" {_counter++}";
        }
    }
}