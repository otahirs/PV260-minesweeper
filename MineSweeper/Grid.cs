using System;

namespace MineSweeper
{
    public class Grid
    {
        private  Cell[,] _grid;
        private int _size;

        public Grid(int size)
        {
            _size = size;
            _grid = new Cell[_size, _size];
            
            for (var row = 0; row < _size; row++)
            for (var col = 0; col < _size; col++)
            {
                _grid[row, col] = new Cell();
            }
        }

        public Cell GetCell(int x, int y)
            => _grid[x, y];
    }
}