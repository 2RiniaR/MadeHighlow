using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Cards;

namespace RineaR.MadeHighlow.Engine.Subjects.Players
{
    /// <summary>
    ///     「プレイヤー」を表現する
    /// </summary>
    public record Player
    {
        /// <summary>
        ///     同一の「ゲーム」における識別子
        /// </summary>
        public ID<Player> ID { get; init; } = ID<Player>.None;

        /// <summary>
        ///     デッキにあるカード
        /// </summary>
        [NotNull]
        public ImmutableList<Card> Cards { get; init; } = ImmutableList<Card>.Empty;
    }
}