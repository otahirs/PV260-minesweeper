using System;

namespace MineSweeper
{
    public static class GridGenerator
    {
        public static Cell[,] Generate(int size, int mineCount)
        {
            if (size < 1)
            {
                throw new ArgumentException();
            }

            if (mineCount > size)
            {
                throw new ArgumentException();
            }
            
            var cells = new Cell[size, size];

            for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
            {
                cells[row, col] = new Cell();
            }

            var r = new Random();
            
            while (mineCount > 0)
            {
                var row = r.Next(0, size);
                var col = r.Next(0, size);

                if (!cells[row, col].IsMine)
                {
                    cells[row, col].IsMine = true;
                    mineCount--;
                }
            }
            
            return cells;
        }
    }
}