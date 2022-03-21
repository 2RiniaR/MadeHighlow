using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles
{
    /// <summary>
    ///     タイル上の「高さ」
    /// </summary>
    public record TileHeight
    {
        protected TileHeight(in TileHeightType type, in bool placeable)
        {
            Type = type;
            Placeable = placeable;
        }

        /// <summary>
        ///     種別
        /// </summary>
        public TileHeightType Type { get; }

        /// <summary>
        ///     オブジェクトを配置できるかどうか
        /// </summary>
        public bool Placeable { get; }

        /// <summary>
        ///     タイル上の「高さ」のうち、「そびえ立つ高さ」
        /// </summary>
        [NotNull]
        public static TileHeight Tower => new(TileHeightType.Tower, false);

        /// <summary>
        ///     タイル上の「高さ」のうち、「奈落」
        /// </summary>
        [NotNull]
        public static TileHeight Abyss => new(TileHeightType.Abyss, false);
    }
}