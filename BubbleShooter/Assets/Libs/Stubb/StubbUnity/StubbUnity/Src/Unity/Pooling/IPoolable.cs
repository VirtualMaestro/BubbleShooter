namespace StubbUnity.Unity.Pooling
{
    public interface IPoolable
    {
        void OnToPool();
        void OnFromPool();
    }
}