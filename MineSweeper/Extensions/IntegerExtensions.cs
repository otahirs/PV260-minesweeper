namespace MineSweeper.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsInRange(this int source, int min, int max)
            => source >= min && source < max;
        public static bool IsInRangeInclusive(this int source, int min, int max)
            => source >= min && source <= max;
    }
}