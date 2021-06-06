using System;

namespace Client.Source.Coins
{
    // Since Unity doesn't know how to handle binary-enums, instead of value it returns index in enum.
    public static class CoinTypeUtil
    {
        private static readonly Array EnumValues;

        static CoinTypeUtil()
        {
            EnumValues = Enum.GetValues(typeof(CoinType));
        }
        
        public static CoinType FixType(CoinType type)
        {
            return (CoinType) EnumValues.GetValue((int)type);
        }

        public static CoinType[] FixTypes(CoinType[] types)
        {
            var fixedTypes = new CoinType[types.Length];

            for (var i = 0; i < types.Length; i++)
            {
                fixedTypes[i] = FixType(types[i]); 
            }
            
            return fixedTypes;
        }
    }
}