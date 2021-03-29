using System;

namespace MineSweeper
{
    public class Grid
    {
        private  Cell[,] _grid;

        public int Size { get; }

        public Grid(int size)
        {
            Size = size;
            _grid = new Cell[Size, Size];
            
            for (var row = 0; row < Size; row++)
            for (var col = 0; col < Size; col++)
            {
                _grid[row, col] = new Cell();
            }
        }

        public Cell GetCell(int x, int y)
            => _grid[x, y];
    }
}