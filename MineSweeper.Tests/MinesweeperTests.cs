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
                { new() },
            };

            var fakeGenerator = A.Fake<IGridGenerator>();
            A.CallTo(() => fakeGenerator.Generate(A<int>._, A<int>._))
                .Returns(cells);

            var game = new MineSweeper(1, fakeGenerator);
            var status = game.PlayTurn(0, 0);

            status.Should().Be(GameStatus.InProgress);
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
        public void TurnPlayedAllDiscovered_Win()
        {
            
        }
        
        [Test]
        public void ZeroCellSelected_NeighboursAreDiscovered()
        {
            //TODO
        }
    }
}