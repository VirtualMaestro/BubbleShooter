using StubbUnity.StubbFramework.View;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// Contains trigger info of 2d physics for Exit phase.
    /// </summary>
    public struct TriggerExit2DComponent
    {
        public IEcsViewLink ObjectA;
        public IEcsViewLink ObjectB;
        public object Info;
    }
}