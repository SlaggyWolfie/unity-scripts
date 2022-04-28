using UnityEngine.Events;

namespace Slaggy.Unity.Extensions
{
    public static class UnityEventExtension
    {
        public static void SetListener<T>(this UnityEvent<T> uEvent, UnityAction<T> call)
        {
            uEvent.RemoveAllListeners();
            uEvent.AddListener(call);
        }
    }
}
