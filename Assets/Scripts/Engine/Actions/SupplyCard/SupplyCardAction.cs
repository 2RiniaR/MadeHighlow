using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SupplyCardAction : Action<SupplyCardResult>
    {
        [NotNull] public PlayerEnsuredID Target { get; init; } = new();
        [NotNull] public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;


        [NotNull]
        public override SupplyCardResult Validate([NotNull] in IActionContext context)
        {
            var player = Target.GetFrom(context.CurrentWorld()) ?? throw new NullReferenceException();
            var deckCapacity = player.DeckSize.Value - player.Cards.Count;

            return new SucceedSupplyCardResult
            {
                Target = Target,
                SuppliedCards = Cards.Select(card => card with { ID = ID.None }).ToValueObjectList(),
            };
        }
    }
}