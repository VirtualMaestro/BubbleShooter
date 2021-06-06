namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Fires when all of the given set of the scenes were unloaded.
    /// </summary>
    public struct ScenesSetUnloadingCompleteEvent
    {
        public string ScenesSetName;
    }
}