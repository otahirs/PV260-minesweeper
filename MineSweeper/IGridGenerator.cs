namespace MineSweeper
{
    public interface IGridGenerator
    {
        Cell[,] Generate(int size, int mineCount);
    }
}