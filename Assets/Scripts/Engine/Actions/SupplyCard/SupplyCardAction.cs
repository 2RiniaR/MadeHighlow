using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

namespace RineaR.MadeHighlow.Actions
{
    public record SupplyCardAction() : Action(ActionType.SupplyCard)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        public SupplyCardResult Run(in ISessionModel session)
        {
            var player = new GetPlayerQuery { Locator = Target }.Run(session.Current());
            var deckCapacity = player.DeckSize.Value - player.Cards.Count;

            return new SucceedSupplyCardResult
            {
                Target = Target,
                SuppliedCards = Cards.Select(card => card with { ID = ID<Card>.None }).ToValueObjectList(),
            };
        }
    }
}