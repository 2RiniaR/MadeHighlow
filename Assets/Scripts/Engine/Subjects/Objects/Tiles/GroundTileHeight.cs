using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles
{
    /// <summary>
    ///     タイル上の「高さ」のうち、「地面」
    /// </summary>
    /// <param name="Placeable">オブジェクトを配置できるかどうか</param>
    public record GroundTileHeight(in bool Placeable = true) : TileHeight(TileHeightType.Ground, Placeable)
    {
        /// <summary>
        ///     高さ
        /// </summary>
        [NotNull]
        public Height Height { get; init; } = new();
    }
}