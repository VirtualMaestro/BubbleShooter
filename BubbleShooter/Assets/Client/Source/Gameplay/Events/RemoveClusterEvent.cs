namespace Client.Source.Gameplay.Events
{
    public struct RemoveClusterEvent
    {
        public int Col;
        public int Row;
        public int Mask;
        public int MinClusterSize;
    }
}