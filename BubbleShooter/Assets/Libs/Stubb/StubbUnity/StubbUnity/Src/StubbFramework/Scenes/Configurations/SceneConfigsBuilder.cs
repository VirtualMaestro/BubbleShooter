using System.Collections.Generic;
using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.StubbFramework.Scenes.Configurations
{
    public class SceneConfigsBuilder<T, S> where T : ILoadingSceneConfig, new() where S : IAssetName, new()
    {
        public static SceneConfigsBuilder<T, S> Create => new SceneConfigsBuilder<T, S>();

        private readonly List<ILoadingSceneConfig> _configs;
        private ILoadingSceneConfig _currentConfig;

        public SceneConfigsBuilder()
        {
            _configs = new List<ILoadingSceneConfig>();
        }

        public SceneConfigsBuilder<T, S> Add(in S sceneName)
        {
            _currentConfig = new T();
            _currentConfig.Name = sceneName;
            _configs.Add(_currentConfig);
            return this;
        }

        /// <summary>
        /// Given config will be cloned.
        /// </summary>
        public SceneConfigsBuilder<T, S> Add(in ILoadingSceneConfig config)
        {
            _currentConfig = config.Clone();
            _configs.Add(_currentConfig);
            return this;
        }

        public SceneConfigsBuilder<T, S> IsActive(bool value = true)
        {
            _currentConfig.IsActive = value;
            return this;
        }

        public SceneConfigsBuilder<T, S> IsMain(bool value = true)
        {
            _currentConfig.IsMain = value;
            return this;
        }

        public SceneConfigsBuilder<T, S> IsMultiple(bool value = true)
        {
            _currentConfig.IsSingle = value;
            return this;
        }

        public SceneConfigsBuilder<T, S> WithPayload(object value)
        {
            _currentConfig.Payload = value;
            return this;
        }

        public List<ILoadingSceneConfig> Build
        {
            get
            {
                _currentConfig = null;
                return _configs;
            }
        }
    }
}