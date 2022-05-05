using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」上での位置
    /// </summary>
    public sealed record Position2D
    {
        [NotNull] public static Position2D Zero => new();
        [NotNull] public Horizontal X { get; init; } = new();
        [NotNull] public Vertical Y { get; init; } = new();

        [NotNull]
        public static Position2D operator +([NotNull] in Position2D l, [NotNull] in Vector2D r)
        {
            return new Position2D { X = l.X + r.X, Y = l.Y + r.Y };
        }

        [NotNull]
        public static Position2D operator -([NotNull] in Position2D l, [NotNull] in Vector2D r)
        {
            return l + -r;
        }

        [NotNull]
        public Position2D MoveTo([NotNull] in Direction2D direction2D, [NotNull] in Distance distance)
        {
            var vector = direction2D.ToVector() * distance.Value;
            return this + vector;
        }

        [CanBeNull]
        public Tile GetTile([NotNull] in World world)
        {
            return world.Tiles.Find(tile => tile.Position2D == this);
        }
    }
}