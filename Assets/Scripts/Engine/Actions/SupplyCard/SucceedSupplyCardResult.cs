using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Cards;

namespace RineaR.MadeHighlow.Actions
{
    public record SucceedSupplyCardResult() : SupplyCardResult(SupplyCardResultCode.Succeed)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] public ValueObjectList<Card> SuppliedCards { get; init; } = ValueObjectList<Card>.Empty;
        [NotNull] public ValueObjectList<Card> OverflowedCards { get; init; } = ValueObjectList<Card>.Empty;
        [NotNull] public ValueObjectList<Card> InvalidCards { get; init; } = ValueObjectList<Card>.Empty;

        public override World Simulate(in World world)
        {
            return new CreateMultiCardsQuery
            {
                ParentLocator = Target,
                Values = SuppliedCards,
            }.Run(world);
        }
    }
}