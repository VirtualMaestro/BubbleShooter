using System;

namespace StubbUnity.Unity.Physics.Settings
{
    [Serializable]
    public class CollisionDispatchProperties
    {
        public bool Enter;
        public bool Stay;
        public bool Exit;

        public bool Enter2D;
        public bool Stay2D;
        public bool Exit2D;
    }
}