using MineSweeper.Configuration;
using MineSweeper.Enums;
using MineSweeper.Extensions;
using System;
using System.Linq;

namespace MineSweeper
{
    public class MineSweeper : IMineSweeper
    {
        private readonly IGridGenerator gridGenerator;
        private Grid grid;

        public MineSweeper(int size, IGridGenerator gridGenerator)
        {
            if (!size.IsInRangeInclusive(GameConfiguration.MinAllowedGridSize, GameConfiguration.MaxAllowedGridSize))
            {
                throw new ArgumentException(
                    $"Size of the game plan has to be between {GameConfiguration.MinAllowedGridSize} " +
                    $"and {GameConfiguration.MaxAllowedGridSize}!");
            }

            this.gridGenerator = gridGenerator;

            InitializeGrid(size);
        }

        private void InitializeGrid(int size)
        {
            var mineCount = GenerateRandomMineCount(size);
            var cells = gridGenerator.Generate(size, mineCount);
            grid = new Grid(size, cells);
        }

        private static int GenerateRandomMineCount(int size)
        {
            var random = new Random();
            var minesPercent = random.Next(
                GameConfiguration.MinMinePercentage,
                GameConfiguration.MaxMinePercentage + 1);

            var mineCount = (int) ((minesPercent / 100.0) * size * size);
            return mineCount;
        }

        public GameStatus PlayTurn(int x, int y, TurnType turnType)
        {
            var cell = grid.GetCell(x, y);

            var gameStatus = turnType switch
            {
                TurnType.DiscoverCell => PlayTurnDiscover(cell),
                TurnType.ToggleFlag => PlayTurnFlag(cell),
                _ => throw new ArgumentException($"Unknown Turn Type '{turnType}'.")
            };

            if (gameStatus != GameStatus.InProgress)
            {
                return gameStatus;
            }

            return CheckIsWin();
        }

        private GameStatus CheckIsWin() 
            => grid.GetCells().All(cell => cell.IsCompleted)
                ? GameStatus.Win 
                : GameStatus.InProgress;
        
        private GameStatus PlayTurnDiscover(Cell cell)
        {
            if (cell.IsMine)
            {
                return GameStatus.Boom;
            }

            DiscoverCells(cell);

            return GameStatus.InProgress;
        }
        
        private GameStatus PlayTurnFlag(Cell cell)
        {
            cell.IsFlagged = !cell.IsFlagged;
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

        public Cell GetCell(int x, int y) 
            => grid.GetCell(x, y);
    }
}