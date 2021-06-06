using System.Collections.Generic;
using StubbUnity.Unity.Scenes;

namespace Client.Source.Common
{
    public static class SceneNames
    {
        private const string ScenePath = "Client/Scenes";

        public static readonly SceneName MainMenuSceneName;
        public static readonly SceneName WinScreenSceneName;
        public static readonly SceneName LoseScreenSceneName;

        private static readonly Dictionary<int, SceneName> _levels = new Dictionary<int, SceneName>();

        static SceneNames()
        {
            MainMenuSceneName = new SceneName("MainMenu", ScenePath);
            WinScreenSceneName = new SceneName("Win", ScenePath);
            LoseScreenSceneName = new SceneName("Lose", ScenePath);

            for (var i = 1; i < 5; i++) _levels[i] = new SceneName($"Level_{i}", "Client/Scenes");
        }

        public static SceneName GetLevel(int level)
        {
            return _levels[level];
        }
    }
}