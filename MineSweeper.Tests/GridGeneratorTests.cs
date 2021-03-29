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
        [TestCase(10, 2)]
        [TestCase(10, 6)]
        public void GivenSize_GeneratedGridHasAllowedNumberOfMines(int size, int mineCount)
        {
            var cells = GridGenerator.Generate(size, mineCount);

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
                { new Cell { IsMine = true }, new Cell { WarnCount = 1 }, new Cell() },
                { new Cell { WarnCount = 3 }, new Cell { WarnCount = 3 }, new Cell { WarnCount = 1 } },
                { new Cell { IsMine = true }, new Cell { IsMine = true }, new Cell { WarnCount = 1 } }
            };
            
            var cells =  new Cell[,]
            {
                { new Cell { IsMine = true }, new Cell(), new Cell() },
                { new Cell(), new Cell(), new Cell() },
                { new Cell { IsMine = true }, new Cell { IsMine = true }, new Cell() }
            };

            GridGenerator.ComputeWarnCount(cells, 3);

            cells.Should().BeEquivalentTo(expectedMatrix);
        }
    }
}