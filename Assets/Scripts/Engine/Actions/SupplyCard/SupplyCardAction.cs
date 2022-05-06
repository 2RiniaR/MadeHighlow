using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーにカードを供給するアクション
    /// </summary>
    public record SupplyCardAction : Action<SupplyCardResult>
    {
        /// <summary>
        ///     供給されるプレイヤー
        /// </summary>
        [NotNull]
        public PlayerID Target { get; init; } = new();

        /// <summary>
        ///     供給するカード
        /// </summary>
        [NotNull]
        public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        public override SupplyCardResult Validate(in IActionContext context)
        {
            var player = Target.GetFrom(context.World) ?? throw new NullReferenceException();
            var deckCapacity = player.DeckSize.Value - player.Cards.Count;

            return new SucceedSupplyCardResult
            {
                Target = Target,
                SuppliedCards = Cards.Select(card => card with { ID = ID.None }).ToValueObjectList(),
            };
        }
    }
}