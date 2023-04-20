using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Extensions
{
    public static class SceneExtension
    {
        public static bool HasController(this Scene scene) => GetController(scene, out _);

        public static bool GetController(this Scene scene, out ISceneController controller)
        {
            controller = null;
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
            {
                if (!go.TryGetComponent(out controller)) continue;

                return true;
            }

            return false;
        }

        public static bool IsNameEqual(this Scene scene, IAssetName name)
        {
            return scene.path.Equals(name.FullName);
        }
        
        public static bool HasScene(this EcsWorld world, in IAssetName sceneName)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (scene.GetController(out var controller) && controller.SceneName.Equals(sceneName))
                    return true;
            }

            return false;
        }
    }
}