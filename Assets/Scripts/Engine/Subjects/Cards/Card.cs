using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Cards.Commands;

namespace RineaR.MadeHighlow.Engine.Subjects.Cards
{
    /// <summary>
    ///     「カード」
    /// </summary>
    public record Card
    {
        public ID<Card> ID { get; init; } = ID<Card>.None;

        /// <summary>
        ///     種類
        /// </summary>
        public CardGenre Genre { get; init; } = CardGenre.Common;

        /// <summary>
        ///     機能
        /// </summary>
        [CanBeNull]
        public Command Command { get; init; } = null;
    }
}