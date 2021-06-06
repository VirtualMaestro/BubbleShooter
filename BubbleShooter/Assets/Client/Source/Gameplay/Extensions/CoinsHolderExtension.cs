using UnityEngine;

namespace Client.Source.Gameplay.Extensions
{
    public static class CoinsHolderExtension
    {
        public static void SetLocalFromGrid(this Transform transform, int col, int row, float coinSize)
        {
            transform.localPosition = GetLocalFromGrid(transform, col, row, coinSize);
        }

        public static Vector3 GetLocalFromGrid(this Transform transform, int col, int row, float coinSize)
        {
            var startColShift = row % 2 == 0 ? coinSize/2 : coinSize;
            var startRowShift = coinSize / 2;
            return new Vector3(startColShift + col*coinSize, -row*coinSize - startRowShift, 0);
        }        
    }
}