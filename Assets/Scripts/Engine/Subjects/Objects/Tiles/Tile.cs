using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「タイル」を表現する
    /// </summary>
    public record Tile() : Object(ObjectType.Tile)
    {
        /// <summary>
        ///     高さ
        /// </summary>
        [NotNull]
        public TileHeight Height { get; init; } = new GroundTileHeight();
    }
}