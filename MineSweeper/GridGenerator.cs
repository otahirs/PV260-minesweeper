using System;

namespace MineSweeper
{
    public static class GridGenerator
    {
        //TODO refactor exceptions
        public static Cell[,] Generate(int size, int mineCount)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size has to be at least 1");
            }

            if (mineCount > size * size)
            {
                throw new ArgumentException("Number of mines has to be" +
                                            " smaller than number of cells in the grid");
            }

            var cells = CreateCells(size);
            FillMines(cells, size, mineCount);
            
            return cells;
        }

        private static void FillMines(Cell[,] cells, int size, int mineCount)
        {
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
        }
        
        private static Cell[,] CreateCells(int size)
        {
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