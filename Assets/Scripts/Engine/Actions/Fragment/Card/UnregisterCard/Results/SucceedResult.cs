using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterCard
{
    public record SucceedResult([NotNull] UnregisterCardAction Action) : UnregisterCardResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            return target.DeleteFrom(world);
        }
    }
}
