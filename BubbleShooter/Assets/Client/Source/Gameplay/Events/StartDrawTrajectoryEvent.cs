using UnityEngine;

namespace Client.Source.Gameplay.Events
{
    // Contains world coordinates
    public struct StartDrawTrajectoryEvent
    {
        public Vector3 Start;
        public Vector3 End;
    }
}