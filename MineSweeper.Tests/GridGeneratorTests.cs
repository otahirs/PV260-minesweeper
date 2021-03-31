using FluentAssertions;
using NUnit.Framework;
using System;

namespace MineSweeper.Tests
{
    public class GridGeneratorTests
    {
        private IGridGenerator gridGenerator;

        [SetUp]
        public void Setup()
        {
            gridGenerator = new GridGenerator();
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(42)]
        public void GivenDesiredSize_GeneratedGridHasCorrectSize(int size)
        {
            var cells = gridGenerator.Generate(size, size);

            cells.Length.Should().Be(size * size);
            cells.GetLength(0).Should().Be(size);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void GivenInvalidSize_GridGeneratorThrowsException(int invalidSize)
        {
            Action act = () => gridGenerator.Generate(invalidSize, invalidSize);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCase(10, 2)]
        [TestCase(10, 6)]
        public void GivenSize_GeneratedGridHasAllowedNumberOfMines(int size, int mineCount)
        {
            var cells = gridGenerator.Generate(size, mineCount);

            int actualMineCount = 0;

            foreach (var cell in cells)
            {
                if (cell.IsMine)
                {
                    actualMineCount++;
                }
            }

            actualMineCount.Should().Be(mineCount);
        }

        [Test]
        public void GivenGrid_NeighbourghMatrixShouldBeValid()
        {
            var expectedMatrix =  new Cell[,]
            {
                { new() { X = 0, Y = 0, IsMine = true }, new() { X = 0, Y = 1, WarnCount = 1 }, new() { X = 0, Y = 2, WarnCount = 0 } },
                { new() { X = 1, Y = 0, WarnCount = 3 }, new() { X = 1, Y = 1, WarnCount = 3 }, new() { X = 1, Y = 2, WarnCount = 1 } },
                { new() { X = 2, Y = 0, IsMine = true }, new() { X = 2, Y = 1, IsMine = true }, new() { X = 2, Y = 2, WarnCount = 1 } },
            };
            
            var cells =  new Cell[,]
            {
                { new() { X = 0, Y = 0, IsMine = true }, new() { X = 0, Y = 1 }, new() { X = 0, Y = 2 } },
                { new() { X = 1, Y = 0 }, new() { X = 1, Y = 1 }, new() { X = 1, Y = 2 } },
                { new() { X = 2, Y = 0, IsMine = true }, new() { X = 2, Y = 1, IsMine = true }, new() { X = 2, Y = 2 } },
            };

            gridGenerator.ComputeWarnCount(cells, 3);

            cells.Should().BeEquivalentTo(expectedMatrix);
        }
    }
}