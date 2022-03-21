using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    /// <summary>
    ///     「フィールド」上でのベクトル
    /// </summary>
    public sealed record Vector2D
    {
        [NotNull] public static Vector2D Zero => new();
        [NotNull] public static Vector2D XPositive => new() { X = 1 };
        [NotNull] public static Vector2D XNegative => new() { X = -1 };
        [NotNull] public static Vector2D YPositive => new() { Y = 1 };
        [NotNull] public static Vector2D YNegative => new() { Y = -1 };
        public int X { get; init; }
        public int Y { get; init; }

        [NotNull]
        public static Vector2D operator +([NotNull] in Vector2D l, [NotNull] in Vector2D r)
        {
            return new Vector2D { X = l.X + r.X, Y = l.Y + r.Y };
        }

        [NotNull]
        public static Vector2D operator -([NotNull] in Vector2D l, [NotNull] in Vector2D r)
        {
            return l + -r;
        }

        [NotNull]
        public static Vector2D operator -([NotNull] in Vector2D l)
        {
            return new Vector2D { X = -l.X, Y = -l.Y };
        }

        [NotNull]
        public static Vector2D operator *([NotNull] in Vector2D l, in int r)
        {
            return new Vector2D { X = l.X * r, Y = l.Y * r };
        }

        [NotNull]
        public Vector2D ExtendTo([NotNull] in Direction2D direction2D, [NotNull] in Distance distance)
        {
            var vector = direction2D.ToVector() * distance.Value;
            return this + vector;
        }
    }
}