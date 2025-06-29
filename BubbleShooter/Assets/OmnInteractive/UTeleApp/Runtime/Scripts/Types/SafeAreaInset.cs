using System;

namespace UTeleApp
{
    [Serializable]
    public struct SafeAreaInset
    {
        public int top;
        public int bottom;
        public int left;
        public int right;
    }

    [Serializable]
    public struct ContentSafeAreaInset
    {
        public int top;
        public int bottom;
        public int left;
        public int right;
    }
}