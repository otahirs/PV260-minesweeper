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
            var g = new Grid(1);
            var cell = g.GetCell(0, 0);
            cell.Should().NotBeNull();
        }

        [Test]
        public void GetCell_OutOfBounds_ThrowsException()
        {
            var g = new Grid(1);

            Action act = () => g.GetCell(42, 42);

            act.Should().Throw<IndexOutOfRangeException>();
        }
    }
}