using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ゲーム全体の状態」を表現する
    /// </summary>
    public record World
    {
        /// <summary>
        ///     次のID生成値
        /// </summary>
        [NotNull]
        public IDGenerator IDGenerator { get; init; } = new();

        /// <summary>
        ///     参加しているプレイヤー
        /// </summary>
        [NotNull]
        public ValueObjectList<Player> Players { get; init; } = ValueObjectList<Player>.Empty;

        /// <summary>
        ///     フィールド上のタイル
        /// </summary>
        [NotNull]
        public ValueObjectList<Tile> Tiles { get; init; } = ValueObjectList<Tile>.Empty;

        /// <summary>
        ///     フィールド上のオブジェクト
        /// </summary>
        [NotNull]
        public ValueObjectList<Entity> Entities { get; init; } = ValueObjectList<Entity>.Empty;

        /// <summary>
        ///     現在のターン
        /// </summary>
        [NotNull]
        public Turn CurrentTurn { get; init; } = new();
    }
}