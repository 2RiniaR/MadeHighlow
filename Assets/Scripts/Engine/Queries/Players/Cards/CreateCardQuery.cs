using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record CreateCardQuery
    {
        [NotNull] public PlayerLocator ParentLocator { get; init; } = new();
        [CanBeNull] public Card Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return new CreateMultiCardsQuery
            {
                ParentLocator = ParentLocator,
                Values = ImmutableList.Create(Value).ToValueObjectList(),
            }.Run(world);
        }
    }
}