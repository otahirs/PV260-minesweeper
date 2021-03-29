using FluentAssertions;
using NUnit.Framework;
using System;

namespace MineSweeper.Tests
{
    public class GridTests
    {
        [Test]
        public void GetCell_IsNotNull()
        {
            var g = new Grid(3, 1);
            var cell = g.GetCell(0, 0);
            cell.Should().NotBeNull();
        }

        [Test]
        public void GetCell_OutOfBounds_ThrowsException()
        {
            var g = new Grid(3, 1);

            Action act = () => g.GetCell(42, 42);

            act.Should().Throw<IndexOutOfRangeException>();
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
    }
}