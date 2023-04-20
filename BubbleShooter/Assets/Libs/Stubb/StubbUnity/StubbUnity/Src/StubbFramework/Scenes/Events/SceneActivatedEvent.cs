namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent when scene become active.
    /// </summary>
    public struct SceneActivatedEvent
    {
        public ISceneController Scene;
    }
}