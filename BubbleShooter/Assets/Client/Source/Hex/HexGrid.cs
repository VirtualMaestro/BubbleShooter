using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Client.Source.Coins;
using StubbUnity.StubbFramework.Logging;
using UnityEngine;

namespace Client.Source.Hex
{
    public class HexGrid
    {
        public enum NeighborOffset
        {
            Left,
            Right,
            BottomLeft,
            BottomRight,
            TopLeft,
            TopRight
        }
        
        // data can be null
        public delegate void GridForEachDelegate(int col, int row, CoinData data = null);
        private CoinData[][] _field;
        private int _cols;
        private int _rows;
        private int _numElements;

        private readonly HashSet<int> _alreadyInProcessingQueue = new HashSet<int>();
        private readonly Queue<CoinData> _processingQueue = new Queue<CoinData>(10);
        private readonly List<CoinData> _neighbors = new List<CoinData>(6);

        private readonly Dictionary<NeighborOffset, Vector2Int> _neighborMapEven;
        private readonly Dictionary<NeighborOffset, Vector2Int> _neighborMapOdd;

        public int Cols => _cols;
        public int Rows => _rows;
        public int Count => _numElements;
        
        public HexGrid()
        {
            _neighborMapEven = new Dictionary<NeighborOffset, Vector2Int>();
            _neighborMapEven[NeighborOffset.Left] = new Vector2Int(-1, 0);
            _neighborMapEven[NeighborOffset.Right] = new Vector2Int(1, 0);
            
            _neighborMapEven[NeighborOffset.TopLeft] = new Vector2Int(-1, -1);
            _neighborMapEven[NeighborOffset.TopRight] = new Vector2Int(0, -1);
            _neighborMapEven[NeighborOffset.BottomLeft] = new Vector2Int(-1, 1);
            _neighborMapEven[NeighborOffset.BottomRight] = new Vector2Int(0, 1);

            
            _neighborMapOdd = new Dictionary<NeighborOffset, Vector2Int>();
            _neighborMapOdd[NeighborOffset.Left] = new Vector2Int(-1, 0);
            _neighborMapOdd[NeighborOffset.Right] = new Vector2Int(1, 0);
            
            _neighborMapOdd[NeighborOffset.TopLeft] = new Vector2Int(0, -1);
            _neighborMapOdd[NeighborOffset.TopRight] = new Vector2Int(1, -1);
            _neighborMapOdd[NeighborOffset.BottomLeft] = new Vector2Int(0, 1);
            _neighborMapOdd[NeighborOffset.BottomRight] = new Vector2Int(1, 1);
        }
        
        public void Init(int columns, int rows)
        {
            _cols = columns;
            _rows = rows;
            
            _field = new CoinData[_rows][];
            
            for (var i = 0; i < _rows; i++)
            {
                _field[i] = new CoinData[_GetCols(i)];
            }
        }

        public bool GetFreePositionByNeighbor(int col, int row, NeighborOffset offset, out Vector2Int resultPosition)
        {
            resultPosition = default;
            var off = row % 2 == 0 ? _neighborMapEven[offset] : _neighborMapOdd[offset];
            var nCol = col + off.x;
            var nRow = row + off.y;

            if (IsValid(nCol, nRow) && IsFree(nCol, nRow))
            {
                resultPosition.Set(nCol, nRow);
                return true;
            }

            return false;
        }

        public Vector2Int GetPositionByNeighbor(int col, int row, NeighborOffset offset)
        {
            var off = row % 2 == 0 ? _neighborMapEven[offset] : _neighborMapOdd[offset];
            var nCol = col + off.x;
            var nRow = row + off.y;

            return new Vector2Int(nCol, nRow);
        }

        /// <summary>
        /// </summary>
        public void GetNotProcessedNeighbors(int col, int row, HashSet<int> processingMap, List<CoinData> neighborsResult)
        {
            // left
            if (Get(col - 1, row, out var coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
                    
            // right
            if (Get(col+1, row, out coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
            
            // top-left
            if (Get(_GetTopLeftColumn(col, row), row-1, out coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
           
            // top-right
            if (Get(_GetTopRightColumn(col, row), row-1, out coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
        
            // bottom-left
            if (Get(_GetBottomLeftColumn(col, row), row+1, out coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
        
            // bottom-right
            if (Get(_GetBottomRight(col, row), row+1, out coinData) && !processingMap.Contains(coinData.ID))
                neighborsResult.Add(coinData);
        }

        public void GetNeighborsByMask(CoinData coinData, List<CoinData> neighborsResult)
        {
            GetNeighborsByMask(coinData.Col, coinData.Row, coinData.Mask, neighborsResult);
        }

        /// <summary>
        /// Fill given neighbors list with neighbors around cell with the type matched to the given type.
        /// </summary>
        public void GetNeighborsByMask(int col, int row, int mask, List<CoinData> neighborsResult)
        {
            // left
            if (GetWithType(col-1, row, mask, out var coinData))
                neighborsResult.Add(coinData);

            // right
            if (GetWithType(col+1, row, mask, out coinData))
                neighborsResult.Add(coinData);

            // top-left
            if (GetWithType(_GetTopLeftColumn(col, row), row-1, mask, out coinData))
                neighborsResult.Add(coinData);

            // top-right
            if (GetWithType(_GetTopRightColumn(col, row), row-1, mask, out coinData))
                neighborsResult.Add(coinData);

            // bottom-left
            if (GetWithType(_GetBottomLeftColumn(col, row), row+1, mask, out coinData))
                neighborsResult.Add(coinData);

            // bottom-right
            if (GetWithType(_GetBottomRight(col, row), row+1, mask, out coinData))
                neighborsResult.Add(coinData);
        }

        public void GetNeighbors(int col, int row, List<CoinData> neighborsResult)
        {
            // left
            if (Get(col-1, row, out var coinData))
                neighborsResult.Add(coinData);

            // right
            if (Get(col+1, row, out coinData))
                neighborsResult.Add(coinData);

            // top-left
            if (Get(_GetTopLeftColumn(col, row), row-1, out coinData))
                neighborsResult.Add(coinData);

            // top-right
            if (Get(_GetTopRightColumn(col, row), row-1, out coinData))
                neighborsResult.Add(coinData);

            // bottom-left
            if (Get(_GetBottomLeftColumn(col, row), row+1, out coinData))
                neighborsResult.Add(coinData);

            // bottom-right
            if (Get(_GetBottomRight(col, row), row+1, out coinData))
                neighborsResult.Add(coinData);
        }

        /// <summary>
        /// Finds all similar coins that border with each other.
        /// </summary>
        public void FindCluster(int col, int row, int mask, List<CoinData> clusterResult)
        {
            if (!Get(col, row, out var coinData))
            {
                log.Warn($"HexGrid.Find wrong position: {col}/{row}");
                return;
            }

            _alreadyInProcessingQueue.Add(coinData.ID);
            _processingQueue.Enqueue(coinData);

            while (_processingQueue.Count > 0)
            {
                var item = _processingQueue.Dequeue();
                
                clusterResult.Add(item);

                GetNeighborsByMask(item.Col, item.Row, mask, _neighbors);

                foreach (var data in _neighbors)
                {
                    if (_alreadyInProcessingQueue.Contains(data.ID)) continue;

                    _alreadyInProcessingQueue.Add(data.ID);
                    _processingQueue.Enqueue(data);
                }
                
                _neighbors.Clear();
            }

            _alreadyInProcessingQueue.Clear();
        }

        public bool RemoveCluster(int col, int row, int mask, int minClusterSize, List<CoinData> clusterResult)
        {
            FindCluster(col, row, mask, clusterResult);

            if (clusterResult.Count >= minClusterSize)
            {
                foreach (var coinData in clusterResult)
                {
                    _Set(coinData.Col, coinData.Row, null);
                }

                return true;
            }
            
            clusterResult.Clear();
            return false;
        }

        /// <summary>
        /// Finds and removed disconnected coins and return a list with removed coins. 
        /// </summary>
        public void RemoveDisconnected(List<CoinData> disconnectedCoins)
        {
            for (var col = 0; col < _cols; col++)
            {
                var coinData = _Get(col, 0);
                if (coinData == null) continue;
                
                _alreadyInProcessingQueue.Add(coinData.ID);
                _processingQueue.Enqueue(coinData);
            }

            while (_processingQueue.Count > 0)
            {
                var coinData = _processingQueue.Dequeue();
                GetNotProcessedNeighbors(coinData.Col, coinData.Row, _alreadyInProcessingQueue, _neighbors);

                foreach (var neighbor in _neighbors)
                {
                    _alreadyInProcessingQueue.Add(neighbor.ID);
                    _processingQueue.Enqueue(neighbor);
                }
                
                _neighbors.Clear();
            }
            
            for (var row = 1; row < _rows; row++)
            {
                var cols = _GetCols(row);
                
                for (var col = 0; col < cols; col++)
                {
                    var coinData = _Get(col, row);
                   if (coinData == null || _alreadyInProcessingQueue.Contains(coinData.ID)) continue;
                   
                   disconnectedCoins.Add(coinData);
                }
            }
            
            foreach (var coinData in disconnectedCoins)
                _Set(coinData.Col, coinData.Row, null);
            
            _alreadyInProcessingQueue.Clear();
        }

        /// <summary>
        /// Removes neighbors including given position. 
        /// </summary>
        public bool RemoveNeighborsSelf(int col, int row, List<CoinData> removedNeighbors)
        {
            GetNeighbors(col, row, removedNeighbors);
            if (removedNeighbors.Count == 0)
                return false;

            foreach (var neighbor in removedNeighbors)
                Remove(neighbor.Col, neighbor.Row);

            if (Remove(col, row, out var data))
                removedNeighbors.Add(data);

            return true;
        }
        
        public void Populate(CoinData[] coins)
        {
            var coinIndex = 0;
            
            for (var i = 0; i < _rows; i++)
            {
                var cols = _GetCols(i);
                for (var j = 0; j < cols; j++)
                {
                    if (coinIndex >= coins.Length) return;

                    var coinData = coins[coinIndex++];
                    Add(j, i, coinData);
                }
            }
        }

        public void ForEach(GridForEachDelegate func)
        {
            for (var i = 0; i < _rows; i++)
            {
                var cols = _GetCols(i);
                
                for (var j = 0; j < cols; j++)
                {
                    func(j, i, _Get(j, i));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add(int col, int row, CoinData data)
        {
            var isValid = IsValid(col, row);
            if (isValid)
            {
                _Set(col, row, data);
                data.Col = col;
                data.Row = row;
            }

            return isValid;
        }

        /// <summary>
        /// Removes element by given col and row.
        /// Returns 'true' if element existed and was removed.
        /// As out param return an element which was removed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(int col, int row, out CoinData data)
        {
            var exists = Get(col, row, out data);
            if (exists) 
                _Set(col, row, null);
            
            return exists;
        }

        /// <summary>
        /// Removes element from grid.
        /// Returns 'true' if removing was successful. 
        /// </summary>
        public bool Remove(int col, int row)
        {
            var isValid = IsValid(col, row);
            if (isValid) 
                _Set(col, row, null);
            
            return isValid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Get(int col, int row, out CoinData data)
        {
            data = null;
            if (!IsValid(col, row) || IsFree(col, row))
                return false;

            data = _Get(col, row);
            return true;
        }

        /// <summary>
        /// Returns data by given col and row only if the type matched with given type. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetWithType(int col, int row, int mask, out CoinData data)
        {
            var exist = Get(col, row, out var coinData);
            exist = exist && coinData.IsOverlap(mask);
            
            data = exist ? coinData : null;
            return exist;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFree(int col, int row) => _Get(col, row) == null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(int col, int row) => row < _rows && row >= 0 && col < _GetCols(row) && col >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int _GetCols(int row) => row % 2 == 0 ? _cols : _cols - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private CoinData _Get(int col, int row)
        {
            return _field[row][col];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _Set(int col, int row, CoinData data)
        {
            _field[row][col] = data;
            _numElements += data == null ? -1 : 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int _GetTopLeftColumn(int col, int row)
        {
            return row % 2 == 0 ? col-1 : col;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int _GetTopRightColumn(int col, int row)
        {
            return row % 2 == 0 ? col : col+1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int _GetBottomLeftColumn(int col, int row)
        {
            return row % 2 == 0 ? col-1 : col;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int _GetBottomRight(int col, int row)
        {
            return row % 2 == 0 ? col : col+1;
        }
        
        //
        public static NeighborOffset GetNeighborOffset(Vector2 hitObjectPosition, Vector2 hitPoint)
        {
            if (hitPoint.x < hitObjectPosition.x && hitPoint.y >= hitObjectPosition.y-0.1)
            {
                return NeighborOffset.Left;
            }
            
            if (hitPoint.x > hitObjectPosition.x && hitPoint.y >= hitObjectPosition.y-0.1)
            {
                return NeighborOffset.Right;
            }
            
            if (hitPoint.x < hitObjectPosition.x && hitPoint.y <= hitObjectPosition.y)
            {
                return NeighborOffset.BottomLeft;
            }
            
            if (hitPoint.x > hitObjectPosition.x && hitPoint.y <= hitObjectPosition.y)
            {
                return NeighborOffset.BottomRight;
            }

            return NeighborOffset.TopLeft; 
        }
    }
}