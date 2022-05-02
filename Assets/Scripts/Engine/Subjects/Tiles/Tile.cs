using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「タイル」を表現する
    /// </summary>
    public record Tile
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID<Tile> ID { get; init; } = ID<Tile>.None;

        /// <summary>
        ///     位置
        /// </summary>
        [NotNull]
        public Position2D Position2D { get; init; } = Position2D.Zero;

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = Direction2D.XPositive;

        /// <summary>
        ///     高さ
        /// </summary>
        [NotNull]
        public TileHeight Height { get; init; } = new GroundTileHeight();

        /// <summary>
        ///     コンポーネント
        /// </summary>
        [NotNull]
        public ValueObjectList<TileComponent> Components { get; init; } = ValueObjectList<TileComponent>.Empty;
    }
}