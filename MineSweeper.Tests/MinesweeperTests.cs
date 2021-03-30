using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;

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
        public void TurnPlayedEmptyCellDiscovered_TurnOK()
        {
            var cells = new Cell[,]
            {
                { new(), new() },

                { new(), new() {IsMine = true} },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(2, fakeGenerator);
            var status = game.PlayTurn(0, 0);

            status.Should().Be(GameStatus.InProgress);
        }

        [Test]
        public void TurnPlayedEmptyCellDiscovered_CellIsDiscovered()
        {
            var cells = new Cell[,]
            {
                { new(), new() },

                { new(), new() {IsMine = true} },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(2, fakeGenerator);
            
            game.PlayTurn(0, 0);

            var cell = game.GetCell(0, 0);
            cell.IsDiscovered.Should().BeTrue();
        }

        [Test]
        public void TurnPlayedMineDiscovered_BoomHappened()
        {
            var cells = new Cell[,] 
            {
                { new() { IsMine = true } },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(1, fakeGenerator);
            var status = game.PlayTurn(0, 0);

            status.Should().Be(GameStatus.Boom);
        }

        [Test]
        public void ZeroCellSelected_NeighboursAreDiscovered()
        {
            var cells = new Cell[,]
            {
                { new(), new(), new() },
                { new(), new(), new() },
                { new(), new(), new() {IsMine = true} },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(3, fakeGenerator);

            game.PlayTurn(0, 0);

            game.GetCell(0, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(0, 1).IsDiscovered.Should().BeTrue();
            game.GetCell(0, 2).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 1).IsDiscovered.Should().BeTrue();
            game.GetCell(1, 2).IsDiscovered.Should().BeTrue();
            game.GetCell(2, 0).IsDiscovered.Should().BeTrue();
            game.GetCell(2, 1).IsDiscovered.Should().BeTrue();

            game.GetCell(2, 1).IsDiscovered.Should().BeFalse();
        }

        [Test]
        public void TurnPlayedAllDiscovered_Win()
        {
            
        }
        

    }
}