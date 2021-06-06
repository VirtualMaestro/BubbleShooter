using StubbUnity.StubbFramework.Scenes.Configurations;

namespace StubbUnity.StubbFramework.Scenes
{
    public interface ISceneLoadingProgress
    {
        /// <summary>
        /// Config data for the loading scene.
        /// </summary>
        ILoadingSceneConfig Config { get; }
        /// <summary>
        /// Returns true when scene was loaded.
        /// </summary>
        bool IsComplete { get; }
        /// <summary>
        /// Return value between 0.0 and 1.0  
        /// </summary>
        float Progress { get; }
        object Payload { get; }
    }
}