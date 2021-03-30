using MineSweeper.Extensions;

namespace MineSweeper.Helpers
{
    public static class CellHelpers
    {
        public static bool IsValidCell(int x, int y, int size)
            => x.IsInRange(0, size) && y.IsInRange(0, size);
    }
}