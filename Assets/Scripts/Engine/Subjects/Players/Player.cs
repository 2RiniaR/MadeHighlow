using JetBrains.Annotations;

namespace RineaR.MadeHighlow
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
        public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        /// <summary>
        ///     デッキの大きさ
        /// </summary>
        [NotNull]
        public PlayerDeckSize DeckSize { get; init; } = new();
    }
}