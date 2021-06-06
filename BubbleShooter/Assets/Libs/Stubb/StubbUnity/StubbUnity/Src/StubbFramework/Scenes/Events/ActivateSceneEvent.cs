using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component can be sent when need to activate scene by its name.
    /// For convenience sake it is better to use World.ActivateSceneByName().
    /// </summary>
    public struct ActivateSceneEvent
    {
        public IAssetName SceneName;
        public bool IsMain;
    }
}