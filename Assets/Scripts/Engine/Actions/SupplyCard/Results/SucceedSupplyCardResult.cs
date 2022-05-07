using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedSupplyCardResult
        ([NotNull] PlayerID TargetPlayerID, [NotNull] Card SupplyCard) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            var player = TargetPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(SupplyCard) };
            return modifiedPlayer.UpdateIn(world);
        }
    }
}