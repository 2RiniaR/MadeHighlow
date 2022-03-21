using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Queries.Players.Cards;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Events.SupplyCard
{
    public record SupplyCardEvent() : Event(EventType.SupplyCard)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] [ItemNotNull] public ImmutableList<Card> Cards { get; init; } = ImmutableList<Card>.Empty;

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