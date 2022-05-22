using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public sealed record SucceedResult(
        [NotNull] ElevateTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileRejection>> RejectionInterrupts
    ) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new AggregateException();
            var modifiedTarget = target with { Elevation = Action.Elevate.Caused(target.Elevation) };
            return modifiedTarget.UpdateIn(world);
        }
    }
}
