using FluentAssertions;
using NUnit.Framework;

namespace MineSweeper.Tests
{
    public class GridTests
    {
        [Test]
        public void GetCell_IsNotNull()
        {
            var g = new Grid(1);
            var cell = g.GetCell(0, 0);
            cell.Should().NotBeNull();
        }

        [Test]
        [TestCase(4)]
        public void GivenNewGrid_AllCellsAreNotDiscovered(int size)
        {
            Grid g = new Grid(size);
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