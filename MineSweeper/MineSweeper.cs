using System;

namespace MineSweeper
{
    public class MineSweeper
    {
        private const int MinePercentageLowerBound = 20;
        private const int MinePercentageUpperBound = 60;
        
        private readonly int mineCount;
        private readonly Grid grid;
        
        public MineSweeper(int size, IGridGenerator gridGenerator)
        {
            if (size < 3 || size > 50)
            {
                throw new ArgumentException("Size of the game plan has to be between" +
                                            "3 and 50!");
            }
            
            var random = new Random();
            var minesPercent = random.Next(MinePercentageLowerBound, MinePercentageUpperBound + 1);
            mineCount = (int) (( minesPercent / 100.0 ) * size * size);

            grid = new Grid(size, mineCount, gridGenerator);
        }

        public GameStatus PlayTurn(int x, int y)
        {
            var cell = grid.GetCell(x, y);
            
            if (cell.IsMine)
            {
                return GameStatus.Boom;
            }

            DiscoverCells(cell);

            return GameStatus.InProgress;
        }

        private void DiscoverCells(Cell cell)
        {
            cell.IsDiscovered = true;
            if (cell.WarnCount != 0) return;

            foreach (var neighbour in grid.GetNeighbours(cell))
            {
                if (!neighbour.IsDiscovered && !neighbour.IsMine)
                {
                    DiscoverCells(neighbour);
                }
            }
        }

        public CellDto GetCell(int x, int y)
        {
            var cell = grid.GetCell(x, y);
            return new CellDto {X = x, Y = y, IsDiscovered = cell.IsDiscovered};
        }
    }
}