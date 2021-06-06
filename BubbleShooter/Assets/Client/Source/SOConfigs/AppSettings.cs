using UnityEngine;

namespace Client.Source.SOConfigs
{
    [CreateAssetMenu (fileName = "AppSettingsSO", menuName = "Game configs/App settings")]
    public class AppSettings : ScriptableObject
    {
        public CoinPrefabs coins;
        public int currentLevel;
    }
}