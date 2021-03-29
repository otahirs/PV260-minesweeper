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
            var cells = GridGenerator.Generate(size);

            cells.Length.Should().Be(size * size);
            cells.GetLength(0).Should().Be(size);
        }

        [Test]
        public void GivenNewGrid_AllCellsAreNotDiscovered()
        {
            const int size = 3;
            var g = new Grid(size);

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