namespace BuildReportTool
{
	[System.Serializable]
	public class BundleEntry
	{
		public string Name;
		public string TotalOutputSize = "";
		public string TotalUserAssetsSize = "";
		public BuildReportTool.SizePart[] BuildSizes;
		public AssetList UsedAssets;
	}
}
