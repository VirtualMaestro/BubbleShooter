using Client.Source.Coins;
using UnityEngine;

namespace Client.Source.SOConfigs
{
    [CreateAssetMenu(fileName = "LevelConfigSO", menuName = "Game configs/Create level config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private CoinType[] boosts;

        public CoinType[] GetBoostTypes => boosts;
        
        public int boardColumns;
        public int boardRows;
        public float coinSize;
        public int minCoinsToDelete;
        
        public int numCoins;

        public GameObject boostIconPrefab;
    }
}