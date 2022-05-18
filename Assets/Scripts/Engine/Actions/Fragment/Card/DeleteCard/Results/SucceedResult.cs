using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public record SucceedResult([NotNull] DeleteCardAction Action) : DeleteCardResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new InvalidOperationException();
            return target.DeleteFrom(world);
        }
    }
}
