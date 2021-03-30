using NUnit.Framework;
using FakeItEasy;
namespace MineSweeper.Tests
{
    public class MinesweeperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public void TurnPlayedEmptyCellDiscovered_TurnOK()
        {
            
        }
        
        
        [Test]
        public void TurnPlayedMineDiscovered_BoomHappened()
        {
            var game = MineSweeper(size: 3, mineCount: 9);
            var status = game.PlayTurn(x, y);
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