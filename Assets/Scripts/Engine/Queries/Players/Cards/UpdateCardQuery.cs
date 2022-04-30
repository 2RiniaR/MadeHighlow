using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record UpdateCardQuery
    {
        [NotNull] public CardLocator Locator { get; init; } = new();
        [CanBeNull] public Card Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            if (Value == null) return world;
            var player = new GetPlayerQuery { Locator = Locator }.Run(world);
            return new UpdatePlayerQuery
            {
                Locator = Locator,
                Value = player with
                {
                    Cards =
                    player.Cards.Items.ReplaceItem(card => card.ID == Locator.CardID, Value).ToValueObjectList(),
                },
            }.Run(world);
        }
    }
}