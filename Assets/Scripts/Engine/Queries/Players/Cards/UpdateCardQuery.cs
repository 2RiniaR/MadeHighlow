using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Cards
{
    public record UpdateCardQuery
    {
        [NotNull] public CardLocator Locator { get; init; } = new();
        [NotNull] public Card Value { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetPlayerQuery { Locator = Locator }.Run(world);
            return new UpdatePlayerQuery
            {
                Locator = Locator,
                Value = player with
                {
                    Cards = player.Cards.ReplaceItem(card => card.ID == Locator.CardID, Value),
                },
            }.Run(world);
        }
    }
}