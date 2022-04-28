namespace Slaggy.Unity.Pooling
{
    public interface IPooledObject
    {
        void OnGetFromPool();
        void OnReturnToPool();
    }
}