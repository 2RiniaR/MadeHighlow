using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public record SucceedResult([NotNull] UnregisterTileAction Action) : UnregisterTileResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            return target.DeleteFrom(world);
        }
    }
}
