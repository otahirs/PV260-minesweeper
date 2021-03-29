using System;
using FluentAssertions;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    public class GridGeneratorTests
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(42)]
        public void GivenDesiredSize_GeneratedGridHasCorrectSize(int size)
        {
            var cells = GridGenerator.Generate(size, size);

            cells.Length.Should().Be(size * size);
            cells.GetLength(0).Should().Be(size);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void GivenInvalidSize_GridGeneratorThrowsException(int invalidSize)
        {
            Action act = () => GridGenerator.Generate(invalidSize, invalidSize);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GivenNewGrid_AllCellsAreNotDiscovered()
        {
            const int size = 3;
            const int mineCount = 1;
            var g = new Grid(size, mineCount);

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var cell = g.GetCell(row, col);
                    cell.IsDiscovered.Should().Be(false);
                }
            }
        }

        [Test]
        [TestCase(10, 2)]
        [TestCase(10, 6)]
        public void GivenSize_GeneratedGridHasAllowedNumberOfMines(int size, int mineCount)
        {
            var cells = GridGenerator.Generate(size, mineCount);

            int actualMineCount = 0;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var cell = cells[row, col];
                    if (cell.IsMine)
                    {
                        actualMineCount++;
                    }
                }
            }

            actualMineCount.Should().Be(mineCount);
        }
    }
}