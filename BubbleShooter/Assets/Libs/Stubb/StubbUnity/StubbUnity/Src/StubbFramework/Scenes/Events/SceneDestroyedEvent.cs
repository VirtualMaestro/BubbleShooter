using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event is sent when scene unloaded and destroyed.
    /// </summary>
    public struct SceneDestroyedEvent
    {
        public IAssetName SceneName;
    }
}