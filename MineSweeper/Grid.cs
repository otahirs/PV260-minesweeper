using System;

namespace MineSweeper
{
    public class Grid
    {
        private  Cell[,] _grid;

        public int Size { get; }

        public Grid(int size, int mineCount, IGridGenerator gridGenerator)
        {
            Size = size;
            _grid = gridGenerator.Generate(size, mineCount);
        }

        public Cell GetCell(int x, int y)
            => _grid[x, y];
    }
}