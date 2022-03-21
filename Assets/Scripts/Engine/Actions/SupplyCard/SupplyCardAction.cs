using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Events.SupplyCard;
using RineaR.MadeHighlow.Engine.Subjects.Cards;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Actions.SupplyCard
{
    public record SupplyCardAction() : Action(ActionType.SupplyCard)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] [ItemNotNull] public ImmutableList<Card> Cards { get; init; } = ImmutableList<Card>.Empty;

        public override EventTimeline Run(in Session session)
        {
            return new EventTimeline(
                new SupplyCardEvent
                {
                    Target = Target,
                    Cards = Cards.Select(card => card with { ID = ID<Card>.None }).ToImmutableList(),
                }
            );
        }
    }
}