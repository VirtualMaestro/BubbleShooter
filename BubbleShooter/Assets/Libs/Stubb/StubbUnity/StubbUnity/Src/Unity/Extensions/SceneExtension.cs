using JetBrains.Annotations;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Names;
using StubbUnity.StubbFramework.Scenes;
using UnityEngine.SceneManagement;

namespace StubbUnity.Unity.Extensions
{
    public static class SceneExtension
    {
        public static bool HasController(this Scene scene) => GetController(scene) != null;

        [CanBeNull]
        public static ISceneController GetController(this Scene scene)
        {
            var gos = scene.GetRootGameObjects();

            foreach (var go in gos)
            {
                var controller = go.GetComponent<ISceneController>();

                if (controller == null) continue;

                return controller;
            }

            return null;
        }

        public static bool IsNameEqual(this Scene scene, IAssetName name)
        {
            return scene.path.Equals(name.FullName);
        }
        
        public static bool HasScene(this EcsWorld world, in IAssetName sceneName)
        {
            for (var i = 1; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var controller = scene.GetController();

                if (controller != null && controller.SceneName.Equals(sceneName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}