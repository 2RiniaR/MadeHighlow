using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ユニット」を表現する
    /// </summary>
    public sealed record Unit : Entity
    {
        /// <summary>
        ///     攻撃力
        /// </summary>
        [NotNull]
        public UnitStrength Strength { get; init; } = new();

        /// <summary>
        ///     メド（一般名：進化の進捗）
        /// </summary>
        [NotNull]
        public UnitMedo Medo { get; init; } = new();

        /// <summary>
        ///     シャドウ（一般名：現在の進化形態）
        /// </summary>
        public UnitShadow Shadow { get; init; } = UnitShadow.Neutral;

        /// <summary>
        ///     フィギュア（一般名：進化する条件）
        /// </summary>
        [NotNull]
        public UnitFigure Figure { get; init; } = UnitFigure.Empty;

        /// <summary>
        ///     所属しているプレイヤーのID
        /// </summary>
        public PlayerID FollowingPlayerID { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public static ValueObjectList<Unit> All([NotNull] in World world)
        {
            return world.Entities.WhereType<Unit>();
        }
    }
}