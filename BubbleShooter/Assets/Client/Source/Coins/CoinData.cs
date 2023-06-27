namespace Client.Source.Coins
{
    public class CoinData
    {
        private static int _idGen;

        public static CoinData Get(CoinType type) => new(type);

        private readonly CoinMask _mask;
        
        public int Col;
        public int Row;

        private CoinType _coinType;
        
        public CoinType CoinType
        {
            get => _coinType;
            set
            {
                _coinType = value;
                _mask.SetType(value);  
            } 
        }
        
        public CoinMonoLink View;

        public int ID { get; }
        public int Mask => _mask.Mask;
        public bool IsOverlap(int mask) => (_mask.Mask & mask) != 0;

        private CoinData(CoinType coinType)
        {
            ID = _idGen++;
            Col = -1;
            Row = -1;
            _mask = new CoinMask();
            CoinType = coinType;
        }

        public void SetPosition(int col, int row)
        {
            Col = col;
            Row = row;
        }
    }
}