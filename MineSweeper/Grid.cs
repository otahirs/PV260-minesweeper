using MineSweeper.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class Grid : IGrid
    {
        private readonly Cell[,] grid;
        private readonly int size; 

        public Grid(int size, Cell[,] cells)
        {
            this.size = size;
            grid = cells;
        }

        public Cell GetCell(int x, int y)
            => grid[x, y];

        public IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            var result = new List<Cell>();

            for (var row = cell.X - 1; row <= cell.X + 1; row++)
            for (var col = cell.Y - 1; col <= cell.Y + 1; col++)
            {
                if (CellHelpers.IsValidCell(row, col, size) && (row != cell.X || col != cell.Y))
                    result.Add(GetCell(row,col));
            }
            return result;
        }

        public IEnumerable<Cell> GetCells() 
            => grid.Cast<Cell>();
    }
}