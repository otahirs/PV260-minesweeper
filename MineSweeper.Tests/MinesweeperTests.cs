using FakeItEasy;
using FluentAssertions;
using MineSweeper.Configuration;
using MineSweeper.Enums;
using NUnit.Framework;
using System;

namespace MineSweeper.Tests
{
    public class MinesweeperTests
    {
        private IGridGenerator gridGenerator;

        [SetUp]
        public void Setup()
        {
            gridGenerator = new GridGenerator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(GameConfiguration.MinAllowedGridSize - 1)]
        [TestCase(GameConfiguration.MaxAllowedGridSize + 1)]
        public void GivenInvalidSizes_ThrowsArgumentException(int size)
        {
            Action act = () => new MineSweeper(size, gridGenerator);

            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        [TestCase(GameConfiguration.MinAllowedGridSize)]
        [TestCase(GameConfiguration.MaxAllowedGridSize)]
        public void GivenValidSizes_CreatesValidMineSweeper(int size)
        {
            Action act = () => new MineSweeper(size, gridGenerator);

            act.Should().NotThrow<Exception>();
        }
        
        [Test]
        public void TurnPlayedEmptyCellDiscovered_TurnOK()
        {
            var cells = new Cell[,]
            {
                { new(), new(), new() },
                { new(), new() {IsMine = true}, new() },
                { new(), new(), new() }
            };
            
            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);
            var status = game.PlayTurn(0, 0, TurnType.DiscoverCell);

            status.Should().Be(GameStatus.InProgress);
        }

        [Test]
        public void TurnPlayedEmptyCellDiscovered_CellIsDiscovered()
        {
            var cells = new Cell[,]
            {
                { new(), new(), new() },
                { new(), new() {IsMine = true}, new() },
                { new(), new(), new() }
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);

            game.PlayTurn(0, 0, TurnType.DiscoverCell);

            var cell = game.GetCell(0, 0);
            cell.IsDiscovered.Should().BeTrue();
        }

        [Test]
        public void TurnPlayedMineDiscovered_BoomHappened()
        {
            var cells = new Cell[,]
            {
                { new() {IsMine = true}, new(), new() },
                { new(), new(), new() },
                { new(), new(), new() }
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);
            var status = game.PlayTurn(0, 0, TurnType.DiscoverCell);

            status.Should().Be(GameStatus.Boom);
        }

        [Test]
        public void ZeroCellSelected_NeighboursAreDiscovered()
        {
            var cells = new Cell[,]
            {
                { new() {X = 0, Y = 0, WarnCount = 0}, new() {X = 0, Y = 1, WarnCount = 0}, new() {X = 0, Y = 2, WarnCount = 0} },
                { new() {X = 1, Y = 0, WarnCount = 0}, new() {X = 1, Y = 1, WarnCount = 1}, new() {X = 1, Y = 2, WarnCount = 1} },
                { new() {X = 2, Y = 0, WarnCount = 0}, new() {X = 2, Y = 1, WarnCount = 1}, new() {X = 2, Y = 2, IsMine = true} },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);

            game.PlayTurn(0, 0, TurnType.DiscoverCell);

            game.GetCell(0, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(0, 1).IsDiscovered.Should().BeTrue();
            game.GetCell(0, 2).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 1).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 2).IsDiscovered.Should().BeTrue();
            game.GetCell(2, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(2, 1).IsDiscovered.Should().BeTrue();

            game.GetCell(2, 2).IsDiscovered.Should().BeFalse();
        }

        [Test]
        public void PutsFlag_CellGetsFlagged()
        {
            var mineSweeper = new MineSweeper(3, gridGenerator);

            mineSweeper.PlayTurn(0, 0, TurnType.ToggleFlag);

            mineSweeper.GetCell(0, 0).IsFlagged.Should().BeTrue();
        }
        
        [Test]
        public void TurnPlayedAllDiscovered_Win()
        {
            var cells = new Cell[,]
            {
                { new() {X = 0, Y = 0, WarnCount = 0, IsDiscovered = true}, new() {X = 0, Y = 1, WarnCount = 0, IsDiscovered = true}, new() {X = 0, Y = 2, WarnCount = 0, IsDiscovered = true} },
                { new() {X = 1, Y = 0, WarnCount = 0, IsDiscovered = true}, new() {X = 1, Y = 1, WarnCount = 1, IsDiscovered = true}, new() {X = 1, Y = 2, WarnCount = 1, IsDiscovered = true} },
                { new() {X = 2, Y = 0, WarnCount = 0, IsDiscovered = true}, new() {X = 2, Y = 1, WarnCount = 1, IsDiscovered = true}, new() {X = 2, Y = 2, IsMine = true} },
            };
            
            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);
            var gameStatus = game.PlayTurn(2, 2, TurnType.ToggleFlag);

            gameStatus.Should().Be(GameStatus.Win);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(0, 2)]
        public void GetCell_RequestedCellInBounds_ReturnsCellCorrectly(int x, int y)
        {
            var cells = new Cell[,]
            {
                { new() {IsMine = true}, new(), new() },
                { new(), new(), new() },
                { new(), new(), new() }
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);
            
            var expectedCell = cells[x, y];
            var actualCell = game.GetCell(x, y);

            actualCell.Should().Be(expectedCell);
        }

        [Test]
        public void GetCell_RequestedCellOutsideBounds_ThrowsException()
        {
            const int size = 3;
            var cells = new Cell[,]
            {
                { new() {IsMine = true}, new(), new() },
                { new(), new(), new() },
                { new(), new(), new() }
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);
            
            Action act = () => game.GetCell(size + 42, size + 42);

            act.Should().Throw<IndexOutOfRangeException>();
        }
    }
}