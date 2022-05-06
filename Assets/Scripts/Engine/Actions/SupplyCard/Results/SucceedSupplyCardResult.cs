using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedSupplyCardResult : SupplyCardResult
    {
        [NotNull] public PlayerEnsuredID Target { get; init; } = new();
        [NotNull] public ValueObjectList<Card> SuppliedCards { get; init; } = ValueObjectList<Card>.Empty;
        [NotNull] public ValueObjectList<Card> OverflowedCards { get; init; } = ValueObjectList<Card>.Empty;
        [NotNull] public ValueObjectList<Card> InvalidCards { get; init; } = ValueObjectList<Card>.Empty;

        public override World Simulate(in World world)
        {
            var player = Target.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.AddRange(SuppliedCards) };
            return modifiedPlayer.UpdateIn(world);
        }
    }
}