using MineSweeper.Extensions;
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

            for (var i = cell.X - 1; i <= cell.X + 1; i++)
            for (var j = cell.Y - 1; j <= cell.Y + 1; j++)
            {
                if (i.IsInRange(0, size) && j.IsInRange(0, size) && (i != cell.X || j != cell.Y))
                    result.Add(GetCell(i,j));
            }
            return result;
        }

        public IEnumerable<Cell> GetCells() 
            => grid.Cast<Cell>();
    }
}