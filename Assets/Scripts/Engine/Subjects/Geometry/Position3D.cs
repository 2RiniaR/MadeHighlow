using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上の3次元座標
    /// </summary>
    public sealed record Position3D([NotNull] Horizontal X, [NotNull] Vertical Y, [NotNull] Height Z)
    {
        [NotNull] public static Position3D Zero => new(new Horizontal(0), new Vertical(0), new Height(0));

        [NotNull]
        public static Position3D operator +([NotNull] Position3D l, [NotNull] Vector3D r)
        {
            return new Position3D(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }

        [NotNull]
        public static Position3D operator -([NotNull] Position3D l, [NotNull] Vector3D r)
        {
            return l + -r;
        }

        [NotNull]
        public Position3D MoveTo([NotNull] Direction3D direction3D, [NotNull] Distance distance)
        {
            var vector = direction3D.ToVector() * distance.Value;
            return this + vector;
        }

        [NotNull]
        public Position2D To2D()
        {
            return new Position2D(X, Y);
        }
    }
}