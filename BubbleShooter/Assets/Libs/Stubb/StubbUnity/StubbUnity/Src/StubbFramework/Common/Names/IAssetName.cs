using System;

namespace StubbUnity.StubbFramework.Common.Names
{
    public interface IAssetName : IEquatable<IAssetName>
    {
        string Name { get; }
        string Path { get; }
        string FullName { get; }
        void Set(string name, string path = null);
        IAssetName Clone();
    }
}