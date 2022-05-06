using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedSupplyCardResult
        ([NotNull] in PlayerID TargetPlayerID, [NotNull] in Card SupplyCard) : SupplyCardResult
    {
        public override World Simulate(in World world)
        {
            var player = TargetPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(SupplyCard) };
            return modifiedPlayer.UpdateIn(world);
        }
    }
}