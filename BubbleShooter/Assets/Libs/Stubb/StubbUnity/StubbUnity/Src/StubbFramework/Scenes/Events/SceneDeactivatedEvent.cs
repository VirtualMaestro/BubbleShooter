namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent when scene become inactive.
    /// </summary>
    public struct SceneDeactivatedEvent
    {
        public ISceneController Scene;
    }
}