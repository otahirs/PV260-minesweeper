using System;
using System.Collections;
using System.Collections.Generic;

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

        public IEnumerable<Cell> GetNeighbours(int x, int y)
        {
            var result = new List<Cell>();
            for (var i = x - 1; i <= x + 1; i++)
            for (var j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < Size && j >= 0 && j < Size && (i != x || j != y))
                    result.Add(GetCell(i,j));
            }
            return result;
        }
    }
}