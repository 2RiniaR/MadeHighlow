using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での3次元ベクトル
    /// </summary>
    public sealed record Vector3D(in int X, in int Y, in int Z)
    {
        [NotNull] public static Vector3D Zero => new(0, 0, 0);
        [NotNull] public static Vector3D XPositive => new(1, 0, 0);
        [NotNull] public static Vector3D XNegative => new(-1, 0, 0);
        [NotNull] public static Vector3D YPositive => new(0, 1, 0);
        [NotNull] public static Vector3D YNegative => new(0, -1, 0);
        [NotNull] public static Vector3D ZPositive => new(0, 0, 1);
        [NotNull] public static Vector3D ZNegative => new(0, 0, -1);

        [NotNull]
        public static Vector3D operator +([NotNull] in Vector3D l, [NotNull] in Vector3D r)
        {
            return new Vector3D(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }

        [NotNull]
        public static Vector3D operator -([NotNull] in Vector3D l, [NotNull] in Vector3D r)
        {
            return l + -r;
        }

        [NotNull]
        public static Vector3D operator -([NotNull] in Vector3D l)
        {
            return new Vector3D(-l.X, -l.Y, -l.Z);
        }

        [NotNull]
        public static Vector3D operator *([NotNull] in Vector3D l, in int r)
        {
            return new Vector3D(l.X * r, l.Y * r, l.Z * r);
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
            return new Vector2D(X, Y);
        }
    }
}