namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Fires for every unloaded scene individually with param name of the unloaded scene.
    /// </summary>
    public struct SceneUnloadingCompleteEvent
    {
        public string SceneName;
    }
}