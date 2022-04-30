using JetBrains.Annotations;

namespace RineaR.MadeHighlow
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
        [NotNull]
        public Command Command { get; init; } = new NoneCommand();
    }
}