using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Queries.Players.Cards
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
                Values = ImmutableList.Create(Value),
            }.Run(world);
        }
    }
}