namespace StubbUnity.StubbFramework.Common
{
    public interface IDestroy
    {
        bool IsDestroyed { get; }
        void Destroy();
    }
}