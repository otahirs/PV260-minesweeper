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
            var random = new Random();
            var minesPercent = random.Next(MinePercentageLowerBound, MinePercentageUpperBound + 1);
            mineCount = (int) (( minesPercent / 100.0 ) * size * size);

            grid = new Grid(size, mineCount, gridGenerator);
        }

        public GameStatus PlayTurn(int x, int y)
        {
            return GameStatus.Boom;
        }
    }
}