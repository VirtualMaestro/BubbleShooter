using StubbUnity.StubbFramework.View;

namespace StubbUnity.StubbFramework.Physics.Components
{
    /// <summary>
    /// Contains collision info of 2d physics for Stay phase.
    /// </summary>
    public struct CollisionStay2DComponent
    {
        public IEcsViewLink ObjectA;
        public IEcsViewLink ObjectB;
        public object Info;
    }
}