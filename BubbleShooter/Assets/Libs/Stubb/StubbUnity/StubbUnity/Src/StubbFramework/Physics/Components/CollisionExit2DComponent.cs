using StubbUnity.StubbFramework.View;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// Contains collision info of 2d physics for Exit phase.
    /// </summary>
    public struct CollisionExit2DComponent
    {
        public IEcsViewLink ObjectA;
        public IEcsViewLink ObjectB;
        public object Info;
    }
}