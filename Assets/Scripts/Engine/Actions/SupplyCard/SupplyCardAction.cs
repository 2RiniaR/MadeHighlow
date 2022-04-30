using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SupplyCardAction() : Action(ActionType.SupplyCard)
    {
        [NotNull] public PlayerLocator Target { get; init; } = new();
        [NotNull] [ItemNotNull] public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        public SupplyCardEvent Run(in Session session)
        {
            return new SupplyCardEvent
            {
                Target = Target,
                Cards = Cards.Items.Select(card => card with { ID = ID<Card>.None }).ToValueObjectList(),
            };
        }
    }
}