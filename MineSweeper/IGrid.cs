using System.Collections.Generic;

namespace MineSweeper
{
    public interface IGrid
    {
        Cell GetCell(int x, int y);
        IEnumerable<Cell> GetNeighbours(Cell cell);
        IEnumerable<Cell> GetCells();
    }
}