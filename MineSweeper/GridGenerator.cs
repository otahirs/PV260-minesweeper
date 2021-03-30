using System;

namespace MineSweeper
{
    public class GridGenerator : IGridGenerator
    {
        //TODO refactor exceptions
        public Cell[,] Generate(int size, int mineCount)
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

        private void FillMines(Cell[,] cells, int size, int mineCount)
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
        
        private Cell[,] CreateCells(int size)
        {
            var cells = new Cell[size, size];

            for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
            {
                cells[row, col] = new Cell {X = row, Y = col};
            }

            return cells;
        }

        public void ComputeWarnCount(Cell[,] cells, int size)
        {
            for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
            {
                if (cells[row, col].IsMine)
                    continue;
                for (var r = row - 1; r <= row + 1; r++)
                for (var c = col - 1; c <= col + 1; c++)
                {
                    if (r >= 0 && r < size && c >= 0 && c < size && cells[r, c].IsMine)
                        cells[row, col].WarnCount++;
                }
            }
        }
    }
}