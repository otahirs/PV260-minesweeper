using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            const int size = 3;
            const int mineCount = 3;
            var cells = gridGenerator.Generate(size, mineCount);
            var g = new Grid(size, cells);

            var cell = g.GetCell(0, 0);
            
            cell.Should().NotBeNull();
        }

        [Test]
        public void GetCell_OutOfBounds_ThrowsException()
        {
            const int size = 3;
            const int mineCount = 3;
            var cells = gridGenerator.Generate(size, mineCount);
            var g = new Grid(size, cells);

            Action act = () => g.GetCell(size + 42, size + 42);

            act.Should().Throw<IndexOutOfRangeException>();
        }
        
        [Test]
        public void GivenNewGrid_AllCellsAreNotDiscovered()
        {
            const int size = 3;
            const int mineCount = 3;
            var cells = gridGenerator.Generate(size, mineCount);
            var g = new Grid(size, cells);

            for (var row = 0; row < size; row++)
            {
                for (var col = 0; col < size; col++)
                {
                    var cell = g.GetCell(row, col);
                    cell.IsDiscovered.Should().Be(false);
                }
            }
        }
        
        [Test]
        public void GivenCellCoords_ReturnCorrectNeighbours()
        {
            const int size = 3;
            const int mineCount = 3;
            var cells = gridGenerator.Generate(size, mineCount);
            var g = new Grid(size, cells);

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

        [Test]
        public void GivenGridCells_GetAllCells_GridReturnsAllCorrectly()
        {
            const int size = 3;
            const int mineCount = 3;

            var cells = gridGenerator.Generate(size, mineCount);
            var grid = new Grid(size, cells);

            var expectedCells = cells.Cast<Cell>();
            var actualCells = grid.GetCells();

            actualCells.Should().BeEquivalentTo(expectedCells);
        }
    }
}