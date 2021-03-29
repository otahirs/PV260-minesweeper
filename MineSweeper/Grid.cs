using System;

namespace MineSweeper
{
    public class Grid
    {
        private  Cell[,] _grid;

        public int Size { get; }

        public Grid(int size, int mineCount)
        {
            Size = size;
            _grid = GridGenerator.Generate(size, mineCount);
        }

        public Cell GetCell(int x, int y)
            => _grid[x, y];
    }
}