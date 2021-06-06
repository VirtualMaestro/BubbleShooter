using System.Text.RegularExpressions;
using StubbUnity.StubbFramework.Common.Names;

namespace StubbUnity.Unity.Scenes
{
    public class SceneName : BaseAssetName
    {
        private static readonly Regex NormalizePathRegex =
            new Regex(@"^\s*/|Assets/|\w+.unity", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public static AssetNamesBuilder<SceneName> Create => new AssetNamesBuilder<SceneName>();

        public SceneName() : base()
        {
        }

        public SceneName(string name, string path = null) : base(name, path)
        {
        }

        protected override string FormatName(string sceneName)
        {
            sceneName = base.FormatName(sceneName);
            sceneName = NormalizePathRegex.Replace(sceneName, string.Empty);

            return sceneName;
        }

        protected override string FormatPath(string path)
        {
            path = base.FormatPath(path);
            path = path.Replace("\\", "/");
            path = NormalizePathRegex.Replace(path, string.Empty);
            path = (path[path.Length - 1] != '/') ? (path + "/") : path;

            return path;
        }

        protected override string FormatFullName(string name, string path)
        {
            return "Assets/" + path + name + ".unity";
        }
        
        public override IAssetName Clone()
        {
            return new SceneName(Name, Path);
        }
    }
}