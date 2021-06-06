using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public interface ILoadingSceneConfig
    {
        IAssetName Name { set; get; }
        /// <summary>
        /// Returns true if after loading, the content will be shown.
        /// </summary>
        bool IsActive { set; get; }
        /// <summary>
        /// Returns true if after loading scene should be marked as main.
        /// </summary>
        bool IsMain { set; get; }
        
        /// <summary>
        /// Allow this scene to be more than one instance.
        /// </summary>
        bool IsSingle { set; get; }
        
        object Payload { set; get; }
        
        ILoadingSceneConfig Clone();
     }
}