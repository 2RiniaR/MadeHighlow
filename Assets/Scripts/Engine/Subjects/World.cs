using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ゲーム全体の状態」を表現する
    /// </summary>
    public record World
    {
        /// <summary>
        ///     最後に生成したID
        /// </summary>
        public ID LatestGeneratedID { get; init; } = ID.None;

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

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList.Concat(
                Players.Select(item => item as IObject),
                Players.SelectMany(player => player.GetChildren()),
                Tiles.Select(item => item as IObject),
                Tiles.SelectMany(player => player.GetChildren()),
                Entities.Select(item => item as IObject),
                Entities.SelectMany(player => player.GetChildren())
            );
        }
    }
}