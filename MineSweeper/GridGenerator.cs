using System;

namespace MineSweeper
{
    public static class GridGenerator
    {
        public static Cell[,] Generate(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException();
            }

            var cells = new Cell[size, size];

            for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
            {
                cells[row, col] = new Cell();
            }

            return cells;
        }
    }
}