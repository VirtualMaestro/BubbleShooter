using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component can be sent when need to deactivate scene by its name.
    /// For convenience sake it is better to use World.DeactivateSceneByName().
    /// </summary>
    public struct DeactivateSceneEvent
    {
        public IAssetName SceneName;
    }
}