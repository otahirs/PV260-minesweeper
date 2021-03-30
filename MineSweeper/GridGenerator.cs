using MineSweeper.Extensions;
using System;

namespace MineSweeper
{
    public class GridGenerator : IGridGenerator
    {
        public Cell[,] Generate(int size, int mineCount)
        {
            if (size < 1)
            {
                throw new ArgumentException("Cannot generate grid of size smaller than 1.");
            }

            if (mineCount > size * size)
            {
                throw new ArgumentException("Number of mines has to be smaller than number of cells in the grid.");
            }

            var cells = CreateCells(size);
            FillMines(cells, size, mineCount);
            
            return cells;
        }

        private Cell[,] CreateCells(int size)
        {
            var cells = new Cell[size, size];

            for (var row = 0; row < size; row++)
            for (var col = 0; col < size; col++)
            {
                cells[row, col] = new Cell { X = row, Y = col };
            }

            return cells;
        }

        private void FillMines(Cell[,] cells, int size, int mineCount)
        {
            var r = new Random();
            
            while (mineCount > 0)
            {
                var row = r.Next(0, size);
                var col = r.Next(0, size);

                if (cells[row, col].IsMine) continue;
                
                cells[row, col].IsMine = true;
                mineCount--;
            }
        }

        public void ComputeWarnCount(Cell[,] cells, int size)
        {
            foreach (var cell in cells)
            {
                if (cell.IsMine)
                    continue;

                for (var row = cell.X - 1; row <= cell.X + 1; row++)
                for (var col = cell.Y - 1; col <= cell.Y + 1; col++)
                {
                    if (row.IsInRange(0, size) && col.IsInRange(0, size) && cells[row, col].IsMine)
                        cell.WarnCount++;
                }
            }
        }
    }
}