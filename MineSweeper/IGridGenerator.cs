namespace MineSweeper
{
    public interface IGridGenerator
    {
        Cell[,] Generate(int size, int mineCount);
        void ComputeWarnCount(Cell[,] cells, int size);
    }
}