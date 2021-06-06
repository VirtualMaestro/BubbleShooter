using System;
using System.Collections.Generic;

namespace Client.Source.Coins
{
    public class CoinMask
    {
        private static readonly Dictionary<CoinType, int> Map;

        static CoinMask()
        {
            Map = new Dictionary<CoinType, int>();
            var values = Enum.GetValues(typeof(CoinType));
            var mask = 1;
            
            for (var i = 0; i < values.Length; i++)
            {
                var key = (CoinType) values.GetValue(i);

                Map.Add(key, key == CoinType.Coin_Wild ? 0xff : mask);

                mask <<= 1;
            }
        }

        private int _mask;
        public int Mask => _mask;

        public void SetType(CoinType type)
        {
            _mask = Map[type];
        }
    }
}