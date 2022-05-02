using JetBrains.Annotations;
using RineaR.MadeHighlow.Personalities;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ユニット」を表現する
    /// </summary>
    public record Unit() : Entity(EntityType.Unit)
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
        [NotNull]
        public Personality Personality { get; init; } = new NonePersonality();

        /// <summary>
        ///     所属しているプレイヤーのID
        /// </summary>
        public ID<Player> FollowingPlayerID { get; init; } = ID<Player>.None;

        /// <summary>
        ///     現在受けている命令
        /// </summary>
        [CanBeNull]
        public UnitOperation CurrentOperation { get; init; }
    }
}