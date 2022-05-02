using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Cards
{
    public record CreateCardQuery
    {
        [NotNull] public PlayerLocator ParentLocator { get; init; } = new();
        [NotNull] public Card Value { get; init; } = new();

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