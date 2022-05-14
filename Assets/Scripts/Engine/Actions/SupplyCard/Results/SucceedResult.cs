using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SucceedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] RegisterCard.SucceedResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PutCard.SucceedResult PutCardResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardEffect>> Interrupts,
        [NotNull] Card Supplied
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            var player = Supplied.OwnerPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(Supplied) };
            return modifiedPlayer.UpdateIn(world);
        }
    }
}
