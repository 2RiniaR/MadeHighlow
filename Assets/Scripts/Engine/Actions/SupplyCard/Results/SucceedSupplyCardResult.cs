using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedSupplyCardResult(
        [NotNull] Card InitialCard,
        [NotNull] RegisterCardResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults,
        [NotNull] Card SuppliedCard
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            var player = SuppliedCard.OwnerPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(SuppliedCard) };
            return modifiedPlayer.UpdateIn(world);
        }
    }
}
