using UnityEngine;

namespace Slaggy.Unity.Examples
{
    public class ExampleNamedArrayScript : MonoBehaviour
    {
        [Properties.NamedArray(new [] { "Name 1", "This is a name", "Name 3", "Hey, it works!"})] 
        public int[] arr = new int[5];
    }
}
