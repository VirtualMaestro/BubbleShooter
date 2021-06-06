namespace StubbUnity.StubbFramework.Time
{
    public interface ITimeService
    {
        long TimeStep { get; }
        long ElapsedMilliseconds { get; }
        long ElapsedFrames { get; }
        void Update();
        void Destroy();
    }
}