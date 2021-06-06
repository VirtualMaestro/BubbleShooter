using System.Runtime.CompilerServices;
using StubbUnity.StubbFramework.Logging;

namespace StubbUnity.StubbFramework.Common.Names
{
    public class BaseAssetName : IAssetName
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string FullName { get; private set; }

        public BaseAssetName()
        {
            Name = string.Empty;
            Path = string.Empty;
        }
        
        public BaseAssetName(string name, string path = null)
        {
            Set(name, path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(string name, string path = null)
        {
            Name = FormatName(name);
            Path = FormatPath(path);
            FullName = FormatFullName(Name, Path);
        }

        public virtual bool Equals(IAssetName other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return FullName.Equals(other.FullName);
        }

        public override string ToString()
        {
            return FullName;
        }

        public virtual IAssetName Clone()
        {
            return new BaseAssetName(Name, Path);
        }

        protected virtual string FormatName(string name)
        {
            name = name.Trim();
            log.Assert(name != string.Empty, $"Incorrect asset name '{name}'!");
            return name;
        }

        protected virtual string FormatPath(string path)
        {
            return (path == null || (path = path.Trim()) == string.Empty) ? string.Empty : path;
        }

        protected virtual string FormatFullName(string name, string path)
        {
            return path + name;
        }
    }
}