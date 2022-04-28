namespace Slaggy.Unity.Extensions
{
    public static class UnityObjectExtensions
    {
        public static T Reference<T>(this T o) where T : UnityEngine.Object => o != null ? o : null;
    }
}
