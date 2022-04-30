using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」上の3次元の位置
    /// </summary>
    public sealed record Position3D
    {
        [NotNull] public static Position3D Zero => new();
        [NotNull] public Horizontal X { get; init; } = new();
        [NotNull] public Vertical Y { get; init; } = new();
        [NotNull] public Height Z { get; init; } = new();

        [NotNull]
        public static Position3D operator +([NotNull] in Position3D l, [NotNull] in Vector3D r)
        {
            return new Position3D { X = l.X + r.X, Y = l.Y + r.Y, Z = l.Z + r.Z };
        }

        [NotNull]
        public static Position3D operator -([NotNull] in Position3D l, [NotNull] in Vector3D r)
        {
            return l + -r;
        }

        [NotNull]
        public Position3D MoveTo([NotNull] in Direction3D direction3D, [NotNull] in Distance distance)
        {
            var vector = direction3D.ToVector() * distance.Value;
            return this + vector;
        }

        [NotNull]
        public Position2D To2D()
        {
            return new Position2D { X = X, Y = Y };
        }
    }
}