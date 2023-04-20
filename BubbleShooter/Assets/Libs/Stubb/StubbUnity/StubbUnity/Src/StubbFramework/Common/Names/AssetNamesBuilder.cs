using System.Collections.Generic;

namespace StubbUnity.StubbFramework.Common.Names
{
    public class AssetNamesBuilder<T> where T: IAssetName, new()
    {
        public List<IAssetName> Build { get; }

        public AssetNamesBuilder()
        {
            Build = new List<IAssetName>();
        }

        public AssetNamesBuilder<T> Add(string name, string path = null)
        {
            IAssetName assetName = new T();
            assetName.Set(name, path);
            Build.Add(assetName);
            
            return this;
        }

        public AssetNamesBuilder<T> Add(IAssetName name)
        {
            Build.Add(name);
            
            return this;
        }
    }
}