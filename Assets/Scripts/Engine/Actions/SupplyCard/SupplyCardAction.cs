using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーにカードを供給するアクション
    /// </summary>
    public record SupplyCardAction
        ([NotNull] PlayerID TargetPlayerID, [NotNull] Card SupplyCard) : Action<SupplyCardResult>
    {
        public override SupplyCardResult Validate(IActionContext context)
        {
            var player = TargetPlayerID.GetFrom(context.World) ?? throw new NullReferenceException();
            var deckCapacity = player.DeckSize.Value - player.Cards.Count;

            return new SucceedSupplyCardResult(TargetPlayerID, SupplyCard);
        }
    }
}