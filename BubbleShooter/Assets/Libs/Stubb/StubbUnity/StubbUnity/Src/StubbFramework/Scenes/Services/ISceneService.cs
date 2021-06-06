using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes.Configurations;

namespace StubbUnity.StubbFramework.Scenes.Services
{
    /// <summary>
    /// Access to engine specific Scene management.
    /// </summary>
    public interface ISceneService
    {
        void Process(ProcessSetScenesConfig config);
        bool HasScene(in IAssetName sceneName);
    }
}