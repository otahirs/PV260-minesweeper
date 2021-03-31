using MineSweeper.Enums;

namespace MineSweeper
{
    public interface IMineSweeper
    {
        GameStatus PlayTurn(int x, int y, TurnType turnType);
        Cell GetCell(int x, int y);
    }
}