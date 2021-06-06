using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.Scenes.Configurations;
using UnityEngine;

namespace StubbUnity.Unity.Scenes
{
    public class SceneLoadingProgress : ISceneLoadingProgress
    {
        public ILoadingSceneConfig Config { get; }
        public bool IsComplete => _async.isDone;
        public float Progress => _async.progress;
        public object Payload { get; }

        private readonly AsyncOperation _async;

        public SceneLoadingProgress(in ILoadingSceneConfig config, in AsyncOperation payload)
        {
            Config = config;
            Payload = payload;
            _async = payload;
        }
    }
}