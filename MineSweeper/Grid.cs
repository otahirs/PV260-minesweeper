using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    public class Grid
    {
        private  Cell[,] _grid;
        public int Size { get; }

        public Grid(int size, int mineCount, IGridGenerator generator)
        {
            Size = size;
            _grid = generator.Generate(size, mineCount);
        }

        public Cell GetCell(int x, int y)
            => _grid[x, y];

        public IEnumerable<Cell> GetNeighbours(Cell cell)
        {
            var result = new List<Cell>();
            for (var i = cell.X - 1; i <= cell.X + 1; i++)
            for (var j = cell.Y - 1; j <= cell.Y + 1; j++)
            {
                if (i >= 0 && i < Size && j >= 0 && j < Size && (i != cell.X || j != cell.Y))
                    result.Add(GetCell(i,j));
            }
            return result;
        }

        public IEnumerable<Cell> GetCells() 
            => _grid.Cast<Cell>();
    }
}