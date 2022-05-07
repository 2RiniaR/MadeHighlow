using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での2次元座標
    /// </summary>
    public sealed record Position2D([NotNull] Horizontal X, [NotNull] Vertical Y)
    {
        [NotNull] public static Position2D Zero => new(new Horizontal(0), new Vertical(0));

        [NotNull]
        public static Position2D operator +([NotNull] Position2D l, [NotNull] Vector2D r)
        {
            return new Position2D(l.X + r.X, l.Y + r.Y);
        }

        [NotNull]
        public static Position2D operator -([NotNull] Position2D l, [NotNull] Vector2D r)
        {
            return l + -r;
        }

        [NotNull]
        public Position2D MoveTo([NotNull] Direction2D direction2D, [NotNull] Distance distance)
        {
            var vector = direction2D.ToVector() * distance.Value;
            return this + vector;
        }

        [CanBeNull]
        public Tile GetTile([NotNull] World world)
        {
            return world.Tiles.Find(tile => tile.Position2D == this);
        }
    }
}
