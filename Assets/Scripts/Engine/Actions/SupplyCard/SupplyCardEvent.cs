using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Players.Cards;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SupplyCardEvent() : Event(ActionType.SupplyCard)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] [ItemNotNull] public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        public override World Simulate(in World world)
        {
            return new CreateMultiCardsQuery
            {
                ParentLocator = Target,
                Values = Cards,
            }.Run(world);
        }
    }
}