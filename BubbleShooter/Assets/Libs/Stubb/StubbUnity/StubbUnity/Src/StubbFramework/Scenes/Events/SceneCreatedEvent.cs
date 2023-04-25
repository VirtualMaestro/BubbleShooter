namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent when scene was loaded and ready to use (might not be active, for this listen for SceneActivatedEvent).
    /// The SceneComponent is already created.
    /// </summary>
    public struct SceneCreatedEvent
    {
        public ISceneController Scene;
    }
}