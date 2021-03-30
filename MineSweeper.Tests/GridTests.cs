using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MineSweeper.Tests
{
    public class GridTests
    {
        private IGridGenerator gridGenerator;

        [SetUp]
        public void Setup()
        {
            gridGenerator = new GridGenerator();
        }

        [Test]
        public void GetCell_IsNotNull()
        {
            var g = new Grid(3, 1, gridGenerator);
            var cell = g.GetCell(0, 0);
            cell.Should().NotBeNull();
        }

        [Test]
        public void GetCell_OutOfBounds_ThrowsException()
        {
            var g = new Grid(3, 1, gridGenerator);

            Action act = () => g.GetCell(42, 42);

            act.Should().Throw<IndexOutOfRangeException>();
        }
        
        [Test]
        public void GivenNewGrid_AllCellsAreNotDiscovered()
        {
            const int size = 3;
            const int mineCount = 1;
            var g = new Grid(size, mineCount, gridGenerator);

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
        public void GivenCellCoords_ReturnCorrectNeighbours()
        {
            var g = new Grid(3, 3, gridGenerator);
            var expectedNeighbours = new List<Cell>
            {
                g.GetCell(0, 0),
                g.GetCell(0, 1),
                g.GetCell(1, 1),
                g.GetCell(2, 0),
                g.GetCell(2, 1)
            };

            g.GetNeighbours(g.GetCell(1, 0)).Should().BeEquivalentTo(expectedNeighbours);
        }
    }
}