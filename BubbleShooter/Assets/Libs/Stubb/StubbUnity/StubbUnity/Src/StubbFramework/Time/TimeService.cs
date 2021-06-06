using System.Diagnostics;

namespace StubbUnity.StubbFramework.Time
{
    public class TimeService : ITimeService
    {
        private Stopwatch _stopwatch;
        private long _timeStep = 0;
        private long _elapsedMilliseconds = 0;
        private long _prevElapsedMilliseconds = 0;
        private long _elapsedFrames = 0;
        
        public TimeService()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        public long TimeStep => _timeStep;
        public long ElapsedMilliseconds => _elapsedMilliseconds;
        public long ElapsedFrames => _elapsedFrames;
        
        public void Update()
        {
            _prevElapsedMilliseconds = _elapsedMilliseconds;
            _elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            _timeStep = _elapsedMilliseconds - _prevElapsedMilliseconds;
            _elapsedFrames++;
        }

        public void Destroy()
        {
            _stopwatch?.Stop();
            _stopwatch = null;
        }
    }
}