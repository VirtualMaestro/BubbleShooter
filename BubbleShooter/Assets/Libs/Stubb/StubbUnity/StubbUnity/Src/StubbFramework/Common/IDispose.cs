namespace StubbUnity.StubbFramework.Common
{
    public interface IDispose
    {
        bool IsDisposed { get; }
        void Dispose();
    }
}