using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units.Personalities;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    /// <summary>
    ///     「ユニット」を表現する
    /// </summary>
    public record Unit() : Object(ObjectType.Unit)
    {
        /// <summary>
        ///     体力
        /// </summary>
        [NotNull]
        public UnitHealth Health { get; init; } = new();

        /// <summary>
        ///     攻撃力
        /// </summary>
        [NotNull]
        public UnitStrength Strength { get; init; } = new();

        /// <summary>
        ///     メド
        /// </summary>
        [NotNull]
        public UnitMedo Medo { get; init; } = new();

        /// <summary>
        ///     フィギュア
        /// </summary>
        public UnitFigure Figure { get; init; } = UnitFigure.Neutral;

        /// <summary>
        ///     性格
        /// </summary>
        [CanBeNull]
        public Personality Personality { get; init; } = null;

        /// <summary>
        ///     所属しているプレイヤーのID
        /// </summary>
        public ID<Player> FollowingPlayerID { get; init; } = ID<Player>.None;

        /// <summary>
        ///     現在受けている命令
        /// </summary>
        [CanBeNull]
        public CommandOperation CurrentOperation { get; init; } = null;
    }
}