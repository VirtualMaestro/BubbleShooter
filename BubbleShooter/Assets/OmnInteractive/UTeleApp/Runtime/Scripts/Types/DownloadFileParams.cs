using System;

namespace UTeleApp
{
    [Serializable]
    public struct DownloadFileParams
    {
        public string url;
        public string file_name;
    }
}