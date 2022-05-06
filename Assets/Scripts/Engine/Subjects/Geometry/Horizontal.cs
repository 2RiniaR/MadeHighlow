using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での横方向の座標
    /// </summary>
    public sealed record Horizontal(in int Value)
    {
        [NotNull]
        public static Horizontal operator +([NotNull] in Horizontal l, in int r)
        {
            return new Horizontal(l.Value + r);
        }

        [NotNull]
        public static Horizontal operator -([NotNull] in Horizontal l, in int r)
        {
            return new Horizontal(l.Value - r);
        }
    }
}