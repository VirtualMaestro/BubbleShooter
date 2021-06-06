using StubbUnity.StubbFramework.View;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// Contains trigger info of 3d physics for Exit phase.
    /// </summary>
    public struct TriggerExitComponent
    {
        public IEcsViewLink ObjectA;
        public IEcsViewLink ObjectB;
        public object Info;
    }
}