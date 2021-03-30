namespace MineSweeper
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsDiscovered { get; set; }
        public bool IsMine { get; set; }
        public int WarnCount { get; set; }
    }
}