using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での縦方向の座標
    /// </summary>
    public sealed record Vertical(int Value)
    {
        [NotNull]
        public static Vertical operator +([NotNull] Vertical l, int r)
        {
            return new Vertical(l.Value + r);
        }

        [NotNull]
        public static Vertical operator -([NotNull] Vertical l, int r)
        {
            return new Vertical(l.Value - r);
        }
    }
}