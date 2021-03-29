namespace MineSweeper
{
    public class Cell
    {
        public bool IsDiscovered { get; set; }

        public bool IsMine { get; set; }
        
        public int WarnCount { get; set; }
    }
}