using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public record SucceedResult([NotNull] UnregisterEntityAction Action) : UnregisterEntityResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            return target.DeleteFrom(world);
        }
    }
}
