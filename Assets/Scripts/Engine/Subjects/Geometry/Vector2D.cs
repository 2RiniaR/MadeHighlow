using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での2次元ベクトル
    /// </summary>
    public sealed record Vector2D(in int X, in int Y)
    {
        [NotNull] public static Vector2D Zero => new(0, 0);
        [NotNull] public static Vector2D XPositive => new(1, 0);
        [NotNull] public static Vector2D XNegative => new(-1, 0);
        [NotNull] public static Vector2D YPositive => new(0, 1);
        [NotNull] public static Vector2D YNegative => new(0, -1);

        [NotNull]
        public static Vector2D operator +([NotNull] in Vector2D l, [NotNull] in Vector2D r)
        {
            return new Vector2D(l.X + r.X, l.Y + r.Y);
        }

        [NotNull]
        public static Vector2D operator -([NotNull] in Vector2D l, [NotNull] in Vector2D r)
        {
            return l + -r;
        }

        [NotNull]
        public static Vector2D operator -([NotNull] in Vector2D l)
        {
            return new Vector2D(-l.X, -l.Y);
        }

        [NotNull]
        public static Vector2D operator *([NotNull] in Vector2D l, in int r)
        {
            return new Vector2D(l.X * r, l.Y * r);
        }

        [NotNull]
        public Vector2D ExtendTo([NotNull] in Direction2D direction2D, [NotNull] in Distance distance)
        {
            var vector = direction2D.ToVector() * distance.Value;
            return this + vector;
        }
    }
}