using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」上でのベクトル
    /// </summary>
    public sealed record Vector3D
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }

        [NotNull] public static Vector3D Zero => new();
        [NotNull] public static Vector3D XPositive => new() { X = 1 };
        [NotNull] public static Vector3D XNegative => new() { X = -1 };
        [NotNull] public static Vector3D YPositive => new() { Y = 1 };
        [NotNull] public static Vector3D YNegative => new() { Y = -1 };
        [NotNull] public static Vector3D ZPositive => new() { Z = 1 };
        [NotNull] public static Vector3D ZNegative => new() { Z = -1 };

        [NotNull]
        public static Vector3D operator +([NotNull] in Vector3D l, [NotNull] in Vector3D r)
        {
            return new Vector3D { X = l.X + r.X, Y = l.Y + r.Y, Z = l.Z + r.Z };
        }

        [NotNull]
        public static Vector3D operator -([NotNull] in Vector3D l, [NotNull] in Vector3D r)
        {
            return l + -r;
        }

        [NotNull]
        public static Vector3D operator -([NotNull] in Vector3D l)
        {
            return new Vector3D { X = -l.X, Y = -l.Y, Z = -l.Z };
        }

        [NotNull]
        public static Vector3D operator *([NotNull] in Vector3D l, in int r)
        {
            return new Vector3D { X = l.X * r, Y = l.Y * r, Z = l.Z * r };
        }

        [NotNull]
        public Vector3D ExtendTo([NotNull] in Direction3D direction3D, [NotNull] in Distance distance)
        {
            var vector = direction3D.ToVector() * distance.Value;
            return this + vector;
        }

        [NotNull]
        public Vector2D To2D()
        {
            return new Vector2D { X = X, Y = Y };
        }
    }
}