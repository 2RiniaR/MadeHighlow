using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record CreateMultiCardsQuery
    {
        [NotNull] public PlayerLocator ParentLocator { get; init; } = new();
        [NotNull] [ItemNotNull] public ValueObjectList<Card> Values { get; init; } = ValueObjectList<Card>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetPlayerQuery { Locator = ParentLocator }.Run(world);
            return new UpdatePlayerQuery
            {
                Locator = ParentLocator,
                Value = player with
                {
                    Cards = player.Cards.Items.AddRange(Values.Items).ToValueObjectList(),
                },
            }.Run(world);
        }
    }
}