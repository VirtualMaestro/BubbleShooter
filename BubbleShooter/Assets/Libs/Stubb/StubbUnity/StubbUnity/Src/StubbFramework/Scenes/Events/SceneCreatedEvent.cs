namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent when scene was loaded and ready to use th SceneComponent to show that scene is ready to use.
    /// </summary>
    public struct SceneCreatedEvent
    {
        public ISceneController Scene;
    }
}